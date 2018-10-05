using System;
using SkiaSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Java.Lang;
using SkiaSharp.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Object = Java.Lang.Object;
using MetroLog;

namespace Sample.SampleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPage : ContentPage
    {
        MainViewModel vm = new MainViewModel();
        Simulation sim = new Simulation();

        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<AnimationPage>();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AnimationPage()
        {
            InitializeComponent();
            Debug.WriteLine("max uint in days: {0} ", UInt32.MaxValue/1000/60/60/24 );
              
            BindingContext = vm;

            Debug.WriteLine("canvas dimensions: {0}/{1}", canvas.Width, canvas.Height);

            sim.MaxX = 1000;
            sim.MaxY = 700;
            //sim.MaxX = canvas.Width;
            //sim.MaxY = canvas.Height;
            log.Error("This is my error");
            if (this.Log.IsInfoEnabled)
                this.Log.Info("I've been navigated to.");

            go.Clicked += delegate
            {
                Play2();
            };

            btnAdd.Clicked += delegate
            {
                sim.AddRandomObject();
                canvas.Elements.Add(sim.Objects[sim.Objects.Count - 1].Shape);
            };

            btnRemove.Clicked += delegate
            {
                sim.Objects.Remove(sim.Objects.Last());
                canvas.Elements.Remove(canvas.Elements.Last());
            };

            AddObjects();
        }

        private void Play2()
        {
            Vector gravity = new Vector(0, 1);

            new Animation((value) =>
            {
                canvas.SuspendLayout();

                foreach (var obj in sim.Objects)
                {
                    if(vm.Gravity == true)
                        obj.MovementVector.addVector(gravity);
                    obj.MovementVector.addVector(obj.Acceleration);
                    obj.Acceleration= new Vector(0,0);
                    obj.Location = new SKPoint((float)(obj.Location.X + obj.MovementVector.x * value), (float)(obj.Location.Y + obj.MovementVector.y * value));
                    //vm.X = obj.Location.X;
                    //lblX.Text = obj._rectangle.X.ToString(); 
                    //canvas.ResumeLayout(true);

                    // inter object gravity

                    foreach (var otherObject  in sim.Objects)
                    {
                        if (otherObject.Equals(obj))
                            continue;
                        if (obj.Shape.Bounds.IntersectsWith(otherObject.Shape.Bounds))
                            continue;
                        obj.AddAttractionVector(otherObject);
                    }

                    // collisions
                    foreach (var otherObject in sim.Objects)
                    {
                        continue;
                        if (otherObject.Equals(obj))
                            continue;
                        if (obj.Shape.Bounds.IntersectsWith(otherObject.Shape.Bounds))
                        {
                            obj.MovementVector.x = 0;
                            obj.MovementVector.y = 0;
                        }
                    }

                    // the floor
                    if (obj.Location.Y > (float)canvas.Height-obj.Shape.Height)
                    {
                        obj.Y= (float) canvas.Height-obj.Shape.Height;
                        obj.MovementVector.y = 0;
                        if (obj.MovementVector.x < -1)
                        {
                            obj.MovementVector.x+=1;
                        }
                        else if (obj.MovementVector.x > 1)
                        {
                            obj.MovementVector.x -= 1;
                        }
                        else
                        {
                            obj.MovementVector.x = 0;
                        }
                    }

                    // the ceiling
                    if (obj.Location.Y < 0)
                    {
                        obj.Y = 0;
                        obj.MovementVector.y = 0;
                        if (obj.MovementVector.x < -1)
                        {
                            obj.MovementVector.x+=1;
                        }
                        else if (obj.MovementVector.x > 1)
                        {
                            obj.MovementVector.x -= 1;
                        }
                        else
                        {
                            obj.MovementVector.x = 0;
                        }
                    }

                    // left wall
                    if (obj.Location.X < 0)
                    {
                        obj.X = 0;
                        obj.MovementVector.x = 0;
                        if (obj.MovementVector.y < -1)
                        {
                            obj.MovementVector.y+=1;
                        }
                        else if (obj.MovementVector.y > 1)
                        {
                            obj.MovementVector.y -= 1;
                        }
                        else
                        {
                            obj.MovementVector.y = 0;
                        }
                    }

                    // right wall
                    if (obj.Location.X > (float)canvas.Width-obj.Shape.Width)
                    {
                        obj.X= (float) canvas.Width-obj.Shape.Width;
                        obj.MovementVector.x = 0;
                        if (obj.MovementVector.y < -1)
                        {
                            obj.MovementVector.y+=1;
                        }
                        else if (obj.MovementVector.y > 1)
                        {
                            obj.MovementVector.y -= 1;
                        }
                        else
                        {
                            obj.MovementVector.y = 0;
                        }
                    }
                }
                    
                canvas.ResumeLayout(true);
            })
            .Commit(this, "Anim", length: 1000000, easing: Easing.Linear, repeat: () => true);
        }

        private void AddObjects()
        {
            sim.Objects.Add(new Circle(500, 500, 500));
            var obj = sim.Objects.Last();
            //obj.X = 800;
            //obj.Y = 300;
            //obj.Size = 200;

            sim.Objects.Last().X = 800;
            sim.Objects.Last().Y = 300;
            sim.Objects.Last().Size = 100;
            sim.Objects.Last().Mass = 5000;
            sim.Objects.Last().Color = SKColors.Black;

            Debug.WriteLine("first object: {0} {1} {2} {3}", sim.Objects.Last().X, sim.Objects.Last().Y, sim.Objects.Last().Size,sim.Objects.Last().Mass);
           

            canvas.Elements.Add(sim.Objects.Last().Shape);

            for (int i = 0; i < 20; i++)
            {
                sim.AddRandomObject();
                canvas.Elements.Add(sim.Objects[sim.Objects.Count-1].Shape);
            }
   
        }

        private void Canvas_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (e.ActionType == SkiaSharp.Views.Forms.SKTouchAction.Pressed)
            {
                Vector Dir = new Vector(sim.Objects[0].X, sim.Objects[0].Y, e.Location.X, e.Location.Y).unitVector();
                Dir.scalarMultiplication(10);
                
                sim.Objects[0].MovementVector.addVector(Dir);
                //lblX.Text = sim.Objects[0].MovementVector.stringify();
            }
        }
    }
}
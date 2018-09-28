using System;
using SkiaSharp;
using System.Collections.Generic;
using System.Diagnostics;
using Java.Lang;
using SkiaSharp.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Object = Java.Lang.Object;

namespace Sample.SampleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPage : ContentPage
    {
        MainViewModel vm = new MainViewModel();
        Simulation sim = new Simulation();

        public AnimationPage()
        {
            InitializeComponent();
            BindingContext = vm;

            Debug.WriteLine("canvas dimensions: {0}/{1}", canvas.Width, canvas.Height);

            sim.MaxX = 500;
            sim.MaxY = 500;
            //sim.MaxX = canvas.Width;
            //sim.MaxY = canvas.Height;

            go.Clicked += delegate
            {
                    Play2();
            };

            btnUp.Clicked += delegate
            {
                ////steerable.Acceleration.y += 5;
                //Debug.WriteLine("clicked");

            };
            btnDown.Clicked += delegate
            {
                //steerable.Acceleration.y-=5;
            };
            btnLeft.Clicked += delegate
            {
                //steerable.Acceleration.x-=5;
            };
            btnRight.Clicked += delegate
            {
                //steerable.Acceleration.x+=5;
            };
      
            AddRectangle();
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
                    obj._rectangle.Location = new SKPoint((float)(obj._rectangle.X+ obj.MovementVector.x * value), (float)(obj._rectangle.Y + obj.MovementVector.y * value));
                    vm.X = obj._rectangle.X;
                    //lblX.Text = obj._rectangle.X.ToString(); 
                    //canvas.ResumeLayout(true);

                    // inter object gravity

                    foreach (var otherObject  in sim.Objects)
                    {
                        
                    }

                    // collisions
                    foreach (var otherObject in sim.Objects)
                    {
                        if (otherObject.Equals(obj))
                            continue;
                        if (obj._rectangle.Bounds.IntersectsWith(otherObject._rectangle.Bounds))
                        {
                            obj.MovementVector.x = 0;
                            obj.MovementVector.y = 0;
                        }
                    }

                    if (obj._rectangle.Y > (float)canvas.Height-obj._rectangle.Height)
                    {
                        obj._rectangle.Y= (float) canvas.Height-obj._rectangle.Height;
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
                }
                    
                canvas.ResumeLayout(true);
            })
            .Commit(this, "Anim", length: 100000, easing: Easing.Linear);
        }

        private void AddRectangle()
        {
            for (int i = 0; i < 100; i++)
            {
                sim.AddRandomObject();
                canvas.Elements.Add(sim.Objects[sim.Objects.Count-1]._rectangle);
            }
        }

        private void Canvas_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (e.ActionType == SkiaSharp.Views.Forms.SKTouchAction.Pressed)
            {
                Vector Dir = new Vector(sim.Objects[0]._rectangle.X, sim.Objects[0]._rectangle.Y, e.Location.X, e.Location.Y).unitVector();
                Dir.scalarMultiplication(10);
                
                sim.Objects[0].MovementVector.addVector(Dir);
                lblX.Text = sim.Objects[0].MovementVector.stringify();
           }
        }
    }
}
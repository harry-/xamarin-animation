using SkiaSharp;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.SampleViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPage : ContentPage
    {
        private Steerable steerable = new Steerable(500, 500);
        
        public AnimationPage()
        {
            InitializeComponent();

            go.Clicked += delegate
            {
                    Play2();
            };

            btnUp.Clicked += delegate { steerable.Acceleration.y--; };
            btnDown.Clicked += delegate { steerable.Acceleration.y++; };
            btnLeft.Clicked += delegate { steerable.Acceleration.x--; };
            btnRight.Clicked += delegate { steerable.Acceleration.x++; };
            

            AddRectangle();
        }

        private void Play2()
        {
            Vector gravity = new Vector(0, 1);

            new Animation((value) =>
            {
                canvas.SuspendLayout();
                    
                //steerable._rectangle.Location = new SKPoint((float)(steerable._rectangle.X+ steerable.UnitDirection.x * value *10), (float)(steerable._rectangle.Y + steerable.UnitDirection.y * value*10));
                steerable.MovementVector.addVector(gravity);
                steerable.MovementVector.addVector(steerable.Acceleration);
                steerable._rectangle.Location = new SKPoint((float)(steerable._rectangle.X+ steerable.MovementVector.x * value), (float)(steerable._rectangle.Y + steerable.MovementVector.y * value));
                canvas.ResumeLayout(true);

                if (steerable._rectangle.Y > (float)canvas.Height-steerable._rectangle.Height)
                {
                    steerable._rectangle.Y= (float) canvas.Height-steerable._rectangle.Height;
                    steerable.MovementVector.y = 0;
                    if (steerable.MovementVector.x < -1)
                    {
                        steerable.MovementVector.x+=1;
                    }
                    else if (steerable.MovementVector.x > 1)
                    {
                        steerable.MovementVector.x -= 1;
                    }
                    else
                    {
                        steerable.MovementVector.x = 0;
                    }
                }
            })
            .Commit(this, "Anim", length: 100000, easing: Easing.Linear);
        }

        private void AddRectangle()
        {
            canvas.Elements.Add(steerable._rectangle); 
        }

        private void Canvas_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (e.ActionType == SkiaSharp.Views.Forms.SKTouchAction.Pressed)
            {
                Vector Dir = new Vector(steerable._rectangle.X, steerable._rectangle.Y, e.Location.X, e.Location.Y).unitVector();
                Dir.scalarMultiplication(10);
                steerable.MovementVector.addVector(Dir );
                direction.Text = steerable.MovementVector.stringify();

                Debug.WriteLine("vector from " + new Vector(steerable._rectangle.X, steerable._rectangle.Y).stringify() + " to " + new Vector(e.Location.X, e.Location.Y).stringify() + ": " + new Vector(steerable._rectangle.X, steerable._rectangle.Y, e.Location.X, e.Location.Y).stringify());
           }
        }
    }
}
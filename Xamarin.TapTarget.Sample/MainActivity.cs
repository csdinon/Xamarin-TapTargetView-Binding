using Android.OS;
using Android.App;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Support.V4.Content;
using Com.Getkeepsafe.Taptargetview;
using Android.Graphics;

namespace Sample.Droid
{
    [Activity(Label = "SampleApp", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private Toolbar _toolbar;
        private IMenuItem _item;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_test, menu);
            _item = menu.FindItem(Resource.Id.menu_search);
            _toolbar.ViewTreeObserver.AddOnGlobalLayoutListener(this);

            return base.OnCreateOptionsMenu(menu);
        }

        public void OnGlobalLayout()
        {
            _toolbar.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);

            Display display = WindowManager.DefaultDisplay;
            Rect bounds = new Rect(0, 0, display.Width, display.Height / 4);
            TapTargetView.ShowFor(this,
                                  TapTarget.ForToolbarMenuItem(_toolbar, Resource.Id.menu_search, "Title", "Description")
                                  .OuterCircleColor(Resource.Color.blue)      // Specify a color for the outer circle
                                  .OuterCircleAlpha(.94f)
                                  .TargetCircleColor(Resource.Color.inner_blue) 
                                  .TitleTextSize(20)                  // Specify the size (in sp) of the title text
                                  .DescriptionTextSize(10)            // Specify the size (in sp) of the description text
                                  .TextColor(Resource.Color.white)    // Specify a color for both the title and description text
                                  .DrawShadow(true)                   // Whether to draw a drop shadow or not
                                  .Icon(ContextCompat.GetDrawable(this, Resource.Drawable.ic_action_search))// Whether tapping outside the outer circle dismisses the view
                                  .TransparentTarget(false)           // Specify whether the target is transparent (displays the content underneath)
                                  .TargetRadius(60));
        }
    }
}


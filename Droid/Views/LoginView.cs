using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using ReactiveUI;
using ReactiveUITest.ViewModels;

namespace ReactiveUITest.Droid.Views
{
    [Activity(Label = "LoginView", MainLauncher = true)]
    public class LoginView : ReactiveActivity<LoginViewModel>
    {
        public LoginView()
        {
            this.WhenActivated(registerDisposable =>
            {
                registerDisposable(this.Bind(this.ViewModel, x => x.Username, x => x.Username.Text));
                registerDisposable(this.Bind(this.ViewModel, x => x.Password, x => x.Password.Text));
                registerDisposable(this.BindCommand(this.ViewModel, x => x.Login, x => x.LoginButton));
                registerDisposable(this.BindCommand(this.ViewModel, x => x.Reset, x => x.Reset, nameof(Reset.Click)));
            });
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LoginLayout);

            this.WireUpControls();
        }

        #region Properties
        public TextInputEditText Username { get; private set; }
        public TextInputEditText Password { get; private set; }
        public Button LoginButton { get; private set; }
        public TextView Reset { get; private set; }
        #endregion
    }
}

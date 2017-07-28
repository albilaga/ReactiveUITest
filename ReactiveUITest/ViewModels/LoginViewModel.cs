using System;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace ReactiveUITest.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {

        public LoginViewModel()
        {
            var canLogin = this.WhenAnyValue(
                x => x.Username, x => x.Password, (user, pass)
                => !string.IsNullOrWhiteSpace(user) && string.IsNullOrEmpty(pass)
                && user.Length >= 6 && pass.Length >= 6);
            this._Login = ReactiveCommand.CreateFromObservable(this.LoginAsync, canLogin);
            this._Reset = ReactiveCommand.Create(() =>
            {
                this.Username = null;
                this.Password = null;
            });
        }

        #region Properties
        private string _Username;
        public string Username
        {
            get { return this._Username; }
            set
            {
                this.RaiseAndSetIfChanged(ref this._Username, value);
            }
        }

        private string _Password;
        public string Password
        {
            get { return this._Password; }
            set { this.RaiseAndSetIfChanged(ref this._Password, value); }
        }
        #endregion

        #region Commands
        private readonly ReactiveCommand<Unit, Unit> _Login;
        public ReactiveCommand<Unit, Unit> Login => this._Login;

        private readonly ReactiveCommand<Unit, Unit> _Reset;
        public ReactiveCommand<Unit, Unit> Reset => this._Reset;
        #endregion

        #region Private Methods
        private IObservable<Unit> LoginAsync() =>
       Observable
           .Return(new Random().Next(0, 2) == 1)
           .Delay(TimeSpan.FromSeconds(1))
           .Do(
               success =>
               {
                   if (!success)
                   {
                       throw new InvalidOperationException("Failed to login.");
                   }
               }
           )
            .Select((arg) => Unit.Default);
        #endregion
    }
}

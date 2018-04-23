using DevExpress.ExpressApp.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp;
using Solution2.Module;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.SystemModule;

namespace Solution2.Win {
    public partial class Solution2WindowsFormsApplication : WinApplication {
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection);
        }
        public Solution2WindowsFormsApplication() {
            InitializeComponent();
        }

        private void Solution2WindowsFormsApplication_DatabaseVersionMismatch( object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e ) {
            if( System.Diagnostics.Debugger.IsAttached ) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                throw new InvalidOperationException(
                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "The automatical update is disabled, because the application was started without debugging.\r\n" +
                    "You should start the application under Visual Studio, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database with the help of the 'DBUpdater' tool." );
            }
        }
        protected override void OnLoggingOn( LogonEventArgs args ) {
            MyLogonParameters parameters = (MyLogonParameters) args.LogonParameters;
            if( parameters.SecurityType == SecurityType.Complex ) {
                ISecurity security = null;
                security = new SecurityComplex( typeof( User ), typeof( Role ), new AuthenticationStandard( typeof( User ), parameters.GetType() ) );
                SecuritySystem.SetInstance( security );
                this.Security = security;
                ( (MyLogonParameters) SecuritySystem.Instance.LogonParameters ).UserName = parameters.UserName;
                ( (MyLogonParameters) SecuritySystem.Instance.LogonParameters ).Password = parameters.Password;
            }
           base.OnLoggingOn( args );
        }
    }
}

using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using Solution2.Module;

namespace Solution2.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            Solution2WindowsFormsApplication application = new Solution2WindowsFormsApplication();
            //SIMPLE
            SecuritySimple _securitySimple = new SecuritySimple();
            _securitySimple.UserType = typeof( BasicUser );

            AuthenticationStandard _authenticationStandard = new AuthenticationStandard();
            _authenticationStandard.LogonParametersType = typeof( MyLogonParameters );
            _authenticationStandard.UserType = typeof( BasicUser );
            _securitySimple.Authentication = _authenticationStandard;
            application.Security = _securitySimple;

            if( ConfigurationManager.ConnectionStrings["ConnectionString"] != null ) {
                application.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            try {
                DevExpress.ExpressApp.InMemoryDataStoreProvider.Register();
                                application.ConnectionString = DevExpress.ExpressApp.InMemoryDataStoreProvider.ConnectionString;
                application.Setup();
                application.Start();
            } catch( Exception e ) {
                application.HandleException( e );
            }
        }
    }
}

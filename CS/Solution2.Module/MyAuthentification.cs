using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security;
using System.Collections.Generic;

namespace Solution2.Module {
    public class MyAuthentication : AuthenticationBase {
        private MyLogonParameters logonParameters;

        public MyAuthentication() {
            logonParameters = new MyLogonParameters();
        }
        public override object Authenticate( DevExpress.ExpressApp.ObjectSpace objectSpace ) {
            if( !logonParameters.User.ComparePassword( logonParameters.Password ) ) {
                throw new AuthenticationException( logonParameters.User.UserName, "Wrong password" );
            }
            return objectSpace.GetObject( logonParameters.User );
        }
        public override void ClearSecuredLogonParameters() {
            logonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }
        public override IList<Type> GetBusinessClasses() {
            return new Type[] { typeof( MyLogonParameters ) };
        }
        public override bool AskLogonParametersViaUI {
            get {
                return true;
            }
        }
        public override object LogonParameters {
            get {
                return logonParameters;
            }
        }
        public override bool IsLogoffEnabled {
            get {
                return false;
            }
        }
    }
}

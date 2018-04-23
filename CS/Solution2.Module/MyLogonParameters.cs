using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Security;

namespace Solution2.Module {
    [NonPersistent]
    public class MyLogonParameters : AuthenticationStandardLogonParameters {
        private SecurityType securityType = SecurityType.Complex;
        public SecurityType SecurityType {
            get {
                return securityType;
            }
            set {
                securityType = value;
            }
        }
    }
    public enum SecurityType {
        Simple = 0,
        Complex = 1
    }
}


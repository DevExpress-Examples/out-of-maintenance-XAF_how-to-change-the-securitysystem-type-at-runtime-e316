Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.Xpo

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Security

Namespace Solution2.Module
	<NonPersistent> _
	Public Class MyLogonParameters
		Inherits AuthenticationStandardLogonParameters
		Private securityType_Renamed As SecurityType = SecurityType.Complex
		Public Property SecurityType() As SecurityType
			Get
				Return securityType_Renamed
			End Get
			Set(ByVal value As SecurityType)
				securityType_Renamed = value
			End Set
		End Property
	End Class
	Public Enum SecurityType
		Simple = 0
		Complex = 1
	End Enum
End Namespace


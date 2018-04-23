Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Windows.Forms

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports Solution2.Module

Namespace Solution2.Win
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
            Dim myApplication As New Solution2WindowsFormsApplication()
			'SIMPLE
			Dim _securitySimple As New SecuritySimple()
			_securitySimple.UserType = GetType(BasicUser)

			Dim _authenticationStandard As New AuthenticationStandard()
			_authenticationStandard.LogonParametersType = GetType(MyLogonParameters)
			_authenticationStandard.UserType = GetType(BasicUser)
			_securitySimple.Authentication = _authenticationStandard
            myApplication.Security = _securitySimple

			If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                myApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
			End If
			Try
                DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.Register()
                                myApplication.ConnectionString = DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.ConnectionString
                myApplication.Setup()
                myApplication.Start()
			Catch e As Exception
                myApplication.HandleException(e)
			End Try
		End Sub
	End Class
End Namespace

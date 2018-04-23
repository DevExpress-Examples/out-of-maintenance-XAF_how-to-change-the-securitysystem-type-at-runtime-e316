Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp
Imports Solution2.Module
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base

Namespace Solution2.Win
	Partial Public Class Solution2WindowsFormsApplication
		Inherits WinApplication
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Solution2WindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
			If System.Diagnostics.Debugger.IsAttached Then
				e.Updater.Update()
				e.Handled = True
			Else
				Throw New InvalidOperationException("The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & Constants.vbCrLf & "The automatical update is disabled, because the application was started without debugging." & Constants.vbCrLf & "You should start the application under Visual Studio, or modify the " & "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & "or manually create a database with the help of the 'DBUpdater' tool.")
			End If
		End Sub
		Protected Overrides Sub OnLoggingOn(ByVal args As LogonEventArgs)
			Dim parameters As MyLogonParameters = CType(args.LogonParameters, MyLogonParameters)
			If parameters.SecurityType = SecurityType.Complex Then
				Dim security As ISecurity = Nothing
				security = New SecurityComplex(GetType(User), GetType(Role), New AuthenticationStandard(GetType(User), parameters.GetType()))
				SecuritySystem.SetInstance(security)
				Me.Security = security
				CType(SecuritySystem.Instance.LogonParameters, MyLogonParameters).UserName = parameters.UserName
				CType(SecuritySystem.Instance.LogonParameters, MyLogonParameters).Password = parameters.Password

				Dim defaultItemNode As DictionaryNode = Model.RootNode.GetChildNode("NavigationItems").FindChildNode("ID", "Default")
				defaultItemNode.FindChildNode("ID", "MyDetails").SetAttribute("ViewID", "User_DetailView")

				Dim RolesNode As New DictionaryNode("Roles")
				RolesNode.AddAttribute("ViewID", "Role_ListView")
				RolesNode.AddAttribute("Caption", "Roles")
				RolesNode.AddAttribute("ImageName", "BO_Role")
				defaultItemNode.AddChildNode(RolesNode)

				Dim UsersNode As New DictionaryNode("Users")
				UsersNode.AddAttribute("ViewID", "User_ListView")
				UsersNode.AddAttribute("Caption", "Users")
				UsersNode.AddAttribute("ImageName", "BO_User")
				defaultItemNode.AddChildNode(UsersNode)
			End If
		   MyBase.OnLoggingOn(args)
		End Sub
	End Class
End Namespace

Imports System
Imports System.Configuration
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl
Imports Solution2.Module

Namespace Solution2.Win

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            Call Windows.Forms.Application.EnableVisualStyles()
            Windows.Forms.Application.SetCompatibleTextRenderingDefault(False)
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
            Dim application As Solution2WindowsFormsApplication = New Solution2WindowsFormsApplication()
            'SIMPLE
            Dim _securitySimple As SecuritySimple = New SecuritySimple()
            _securitySimple.UserType = GetType(BasicUser)
            Dim _authenticationStandard As AuthenticationStandard = New AuthenticationStandard()
            _authenticationStandard.LogonParametersType = GetType(MyLogonParameters)
            _authenticationStandard.UserType = GetType(BasicUser)
            _securitySimple.Authentication = _authenticationStandard
            application.Security = _securitySimple
            If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                application.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            End If

            Try
                Xpo.InMemoryDataStoreProvider.Register()
                application.ConnectionString = Xpo.InMemoryDataStoreProvider.ConnectionString
                application.Setup()
                application.Start()
            Catch e As Exception
                application.HandleException(e)
            End Try
        End Sub
    End Module
End Namespace

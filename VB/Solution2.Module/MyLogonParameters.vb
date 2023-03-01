Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Security

Namespace Solution2.Module

    <NonPersistent>
    Public Class MyLogonParameters
        Inherits AuthenticationStandardLogonParameters

        Private securityTypeField As SecurityType = SecurityType.Complex

        Public Property SecurityType As SecurityType
            Get
                Return securityTypeField
            End Get

            Set(ByVal value As SecurityType)
                securityTypeField = value
            End Set
        End Property
    End Class

    Public Enum SecurityType
        Simple = 0
        Complex = 1
    End Enum
End Namespace

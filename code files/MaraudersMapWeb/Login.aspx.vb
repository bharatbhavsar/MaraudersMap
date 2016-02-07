Imports MaraudersMapBLL
Imports System.Net.Mail
Public Class Login1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Expires = -1
        'netsh http add urlacl url=http://{ip_addr}:{port}/ user=everyone
        'get the q parameter from URL
        Dim type = UCase(Request.QueryString("type"))


        Dim usercont As New LoginRegistrationController1
        Dim user As New UserProp
        user.NET_ID = UCase(Request.QueryString("netid"))
        
        If String.Compare(type, "LOGIN") = 0 Then
            user.Password = (Request.QueryString("password"))

            '          MsgBox("login")
            Response.Write(usercont.login(user))
        ElseIf String.Compare(type, "REGISTRATION") = 0 Then
            user.Password = (Request.QueryString("password"))
            user.First_Name = UCase(Request.QueryString("fname"))
            user.Last_Name = UCase(Request.QueryString("lname"))
            user.Facebook_ID = UCase(Request.QueryString("fb"))
            user.LinkedIn_URL = UCase(Request.QueryString("linkedin"))
            user.Mobile_No = UCase(Request.QueryString("mobile"))
            '         MsgBox("reg")
            Response.Write(usercont.createUser(user))

        ElseIf String.Compare(type, "VALIDATE") = 0 Then
            Response.Write(usercont.validate(user, UCase(Request.QueryString("passcode"))))
        End If


    End Sub
End Class
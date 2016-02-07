Imports System.Net.Mail
Public Class LoginRegistrationController1
    Function sendMail(ByVal userobj As UserProp, ByVal passcode As String) As Boolean
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("utdmaraudersmap@gmail.com", "")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("utdmaraudersmap@gmail.com")
            e_mail.To.Add(userobj.NET_ID & "@utdallas.edu")
            e_mail.Subject = "Confirmation Email"
            e_mail.IsBodyHtml = True
            e_mail.Body = "<h3>Welcome to UTD Marauder's Map </h3><br/>Please confirm your email address by clicking <a href=""http://utdmaraudersmap.somee.com/maraudersmapweb/Login.aspx?type=validate&netid=" & userobj.NET_ID & "&passcode=" & passcode & """>here</a> <br/><br/><br/> If you have any questions or feedback, send it to us at utdmaraudersmap@gmail.com<br/><h2><br/>Best,<br>Marauder's Map Team<br></h2>"
            Smtp_Server.Send(e_mail)
            'MsgBox("Mail Sent")
            Return True
        Catch error_t As Exception
            ' MsgBox(error_t.ToString)
            Return False
        End Try

    End Function
    Function createUser(ByVal userobj As UserProp) As Boolean

        Dim dataobj As New DAL_Functions
        Dim rand As New Random

        Dim passcode As String
        passcode = rand.Next(1, 999999).ToString
        If dataobj.createUserInDB(userobj, passcode) Then
            sendMail(userobj, passcode)
            Return True
        End If
        Return False
    End Function

    Function login(ByVal userobj As UserProp) As String
        Dim dataobj As New DAL_Functions
        Dim response = ""
        If dataobj.isValidated(userobj) Then

            Dim newuserobj As New UserProp
            newuserobj = dataobj.getUserByIDFromDB(userobj.NET_ID)
            '  MsgBox(newuserobj.Password)
            If String.Compare((newuserobj.Password), (userobj.Password)) = 0 Then
                Return "True"
            Else
                Return "Wrong username password combination"
            End If
        Else
            Return "User Not Yet Validated"
        End If

    End Function
    Function validate(ByVal userobj As UserProp, ByVal passcode As String) As Boolean
        Dim dataobj As New DAL_Functions
        Return dataobj.validate(userobj, passcode)
    End Function
End Class

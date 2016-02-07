Imports MaraudersMapBLL
Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.AppendHeader("Access-Control-Allow-Origin", "*")
        Response.Expires = -1
        'netsh http add urlacl url=http://{ip_addr}:{port}/ user=everyone
        'get the q parameter from URL
        Dim privacy = UCase(Request.QueryString("privacy"))
        Dim usercont As New UserController
        Dim user As New UserDynamicLocationProp
        user.NET_ID = UCase(Request.QueryString("netid"))

        If String.Compare(privacy, "1") = 0 Then

            user.User_Latitude = UCase(Request.QueryString("latitude"))
            user.User_Longitude = UCase(Request.QueryString("longitude"))
            user.User_Altitude = UCase(Request.QueryString("altitude"))
            user.User_Accuracy = UCase(Request.QueryString("accuracy"))
            user.User_TimeStamp = UCase(Request.QueryString("timestamp"))
            usercont.UpdateLocation(user)

        End If
        Dim alluserloc() As UserDynamicLocationProp = usercont.getAllUserLocations(user)
        Dim res As String = "["

        For i = 1 To alluserloc.Length - 1
            'Dim userloc As UserDynamicLocationProp = alluserloc(i)
            res = res & " { ""netid"" : """ + alluserloc(i).NET_ID & """, ""latitude"" : """ & alluserloc(i).User_Latitude & """, ""longitude"" : """ & alluserloc(i).User_Longitude & """ , ""altitude"" : """ & alluserloc(i).User_Altitude & """ , ""accuracy"" : """ & alluserloc(i).User_Accuracy & """ , ""timestamp"" : """ & alluserloc(i).User_TimeStamp & """},"

        Next
        res = res.Substring(0, res.Length - 1)
        res = res + "]"
        Response.Write(res)

    End Sub
End Class
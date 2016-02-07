Imports MaraudersMapBLL
Public Class Search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Expires = -1
        'netsh http add urlacl url=http://{ip_addr}:{port}/ user=everyone
        'get the q parameter from URL
        Dim usercont As New UserController
        If String.Compare(UCase(Request.QueryString("type")), "SEARCH") = 0 Then
            Dim ads As Object() = usercont.search(Request.QueryString("searchtext"))
            '  MsgBox(Request.QueryString("searchtext"))
            Dim res As String = "["

            Dim userDetail() As UserProp = ads(2)

            For i = 1 To userDetail.Length - 1
                res = res & " { ""type"" : ""user"", ""netid"" : """ + userDetail(i).NET_ID & """, ""fname"" : """ & userDetail(i).First_Name & """, ""lname"" : """ & userDetail(i).Last_Name & """ , ""mobile"" : """ & userDetail(i).Mobile_No & """ , ""facebook"" : """ & userDetail(i).Facebook_ID & """ , ""linkedin"" : """ & userDetail(i).LinkedIn_URL & """},"

            Next
            Dim placeDetail() As PlaceDetailsProp = ads(1)

            For i = 1 To placeDetail.Length - 1
                res = res & " { ""type"" : ""place"", ""placeid"" : """ + placeDetail(i).Place_ID & """, ""placename"" : """ & placeDetail(i).Place_Name & """, ""placeemail"" : """ & placeDetail(i).Place_Email & """ , ""placephone"" : """ & placeDetail(i).Place_Phone & """ , ""placehours"" : """ & placeDetail(i).Place_Hours & """},"

            Next
            res = res.Substring(0, res.Length - 1)
            res = res & "]"
            Response.Write(res)
        ElseIf String.Compare(UCase(Request.QueryString("type")), "LOCATION") = 0 Then
            Dim userlocation As UserDynamicLocationProp = usercont.getLocation(Request.QueryString("id"))
            Dim res As String = "[{"
            res = res & " ""latitude"" : """ & userlocation.User_Latitude & """ , ""longitude"" : """ & userlocation.User_Longitude & """ "
            res = res & "}]"
            Response.Write(res)
        Else
            Response.Write("INVALID")
        End If

    End Sub

End Class
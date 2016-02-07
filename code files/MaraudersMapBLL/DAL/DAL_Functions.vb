Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Public Class DAL_Functions
    Dim log As New Logger

    Dim connectionString As String = "workstation id=utdmaraudersmap.mssql.somee.com;packet size=4096;user id=utdmaraudersmap_SQLLogin_1;pwd=tst6jsb145;data source=utdmaraudersmap.mssql.somee.com;persist security info=False;initial catalog=utdmaraudersmap"
    Dim Connection As New SqlConnection(connectionString)
    Function reportError(ByVal e As Exception)
        ' MsgBox(e.ToString)
        Dim saveUtcNow As DateTime = DateTime.UtcNow
        Dim errorCommand As New SqlCommand("", Connection)                              'Query Entered ""
        Try
            errorCommand.ExecuteNonQuery()
        Catch

        End Try
    End Function
    Function validate(ByVal userobj As UserProp, ByVal passcode As String) As Boolean
        Dim res = 0
        Dim command As New SqlCommand("", Connection)                         'Query Entered ""
        Try
            Connection.Open()
            res = command.ExecuteNonQuery()
        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        If res = 1 Then
            Return True
        End If

        Return False
    End Function
    Function isValidated(ByVal user As UserProp) As Boolean
        Dim command As New SqlCommand("", Connection)                              'Query Entered ""
        Dim count As String = ""
        Try
            Connection.Open()
            Dim res As SqlDataReader = command.ExecuteReader()
            res.Read()
            count = res.Item(0)
        Catch e As Exception
            reportError(e)

        Finally
            Connection.Close()
        End Try

        If String.Compare(count, "1") = 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Function getUserLocationFromDB(ByVal NetID As String) As UserDynamicLocationProp
        '    log.append("entered getUserLocationFromDB", "info")

        Dim UserLocation As New UserDynamicLocationProp
        Try
            Dim command As New SqlCommand("", Connection)                              'Query Entered ""
            Connection.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()

                UserLocation.NET_ID = reader.Item("").ToString()
                UserLocation.User_Latitude = reader.Item("").ToString()
                UserLocation.User_Longitude = reader.Item("").ToString()
                UserLocation.User_Altitude = reader.Item("").ToString()
                UserLocation.User_TimeStamp = reader.Item("").ToString()
                UserLocation.User_Accuracy = reader.Item("").ToString()
                UserLocation.User_Altitude_Accuracy = reader.Item("").ToString()
            End While
        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        Return UserLocation

    End Function
    Function getLocation(ByVal id As String) As UserDynamicLocationProp
        Dim userlocation As New UserDynamicLocationProp
        userlocation = getUserLocationFromDB(id)
        If String.Compare(UCase(userlocation.NET_ID), UCase(id)) = 0 Then
        Else
            Dim placelocation As New PlaceProp
            placelocation = getPlaceLocationFromDB(id)
            userlocation.User_Latitude = placelocation.Place_Latitude
            userlocation.User_Longitude = placelocation.Place_Longitude
            userlocation.NET_ID = placelocation.Place_ID
        End If

        Return userlocation
    End Function
    Function getUserByIDFromDB(ByVal ID As String) As UserProp
        Dim UserInfo As New UserProp

        Try
            Dim command As New SqlCommand("", Connection)                              'Query in ""
            Connection.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()
            If reader.Read() Then

                UserInfo.NET_ID = reader.Item("").ToString()
                UserInfo.First_Name = reader.Item("").ToString()
                UserInfo.Last_Name = reader.Item("").ToString()
                UserInfo.Mobile_No = reader.Item("").ToString()
                UserInfo.LinkedIn_URL = reader.Item("").ToString()
                UserInfo.Facebook_ID = reader.Item("").ToString()
                UserInfo.Password = reader.Item("").ToString()

            Else
                UserInfo = Nothing
            End If

        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        Return UserInfo
    End Function
    Function getUserFromDB(ByVal Name As String) As UserProp()   'Refine to merge result set
        Dim UserInfo(0) As UserProp 'Get Count from query and use it to define size of this array
        Try
            Dim command As New SqlCommand("", Connection)                              'Query in """
            Connection.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()
            Dim i As Integer = 0
            While reader.Read()
                i = i + 1
                ReDim Preserve UserInfo(i)
                Dim UserDetails As New UserProp
                UserDetails.NET_ID = reader.Item("NET_ID").ToString()
                UserDetails.First_Name = reader.Item("First_Name").ToString()
                UserDetails.Last_Name = reader.Item("Last_Name").ToString()
                UserDetails.Mobile_No = reader.Item("Mobile_No").ToString()
                UserDetails.LinkedIn_URL = reader.Item("LinkedIn_URL").ToString()
                UserDetails.Facebook_ID = reader.Item("Facebook_id").ToString()

                UserInfo(i) = UserDetails

            End While
        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        Return UserInfo
    End Function

    Function getPlaceLocationFromDB(ByVal Place_ID As String) As PlaceProp
        Dim Place As New PlaceProp 'Get Count from query and use it to define size of this array
        Try
            Dim command As New SqlCommand("", Connection)                              'Query in ""
            Connection.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                Place.Place_ID = reader.Item("P_ID").ToString()
                Place.Place_Name = reader.Item("PNAME").ToString()
                Place.Place_Type = reader.Item("P_TYPE").ToString()
                Place.Place_Latitude = reader.Item("Latitude").ToString()
                Place.Place_Longitude = reader.Item("Longitude").ToString()
            End While

        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        Return Place
    End Function
    Function getPlaceDetailsFromDB(ByVal PlaceInfoInput As String) As PlaceDetailsProp()
        Dim PlaceInfo(0) As PlaceDetailsProp
        Try
            Dim command As New SqlCommand("", Connection)                              'Query in ""
            Connection.Open()
            Dim i As Integer = 0
            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                i = i + 1
                ReDim Preserve PlaceInfo(i)
                Dim PlaceDetails As New PlaceDetailsProp
                PlaceDetails.Place_ID = reader.Item(0).ToString()
                PlaceDetails.Place_Name = reader.Item(1).ToString()

                PlaceDetails.Place_Email = reader.Item(2).ToString()
                PlaceDetails.Place_Phone = reader.Item(3).ToString()
                PlaceDetails.Place_Hours = reader.Item(4).ToString()
                PlaceInfo(i) = PlaceDetails

            End While

        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        Return PlaceInfo
    End Function
    Function updateLocationToDB(ByVal userLocation As UserDynamicLocationProp) As Boolean

        Dim count As Integer
        Try
            Dim command As New SqlCommand("", Connection)                              'Query in ""
            Connection.Open()
            count = command.ExecuteNonQuery()
        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        If count = 1 Then
            '          Response.write("Updated Location to DB updateLocationToDB", "info")

            Return True
        End If

        Return False
    End Function
    Function getOtherUserLocationsFromDB(ByVal users As UserProp()) As UserDynamicLocationProp()


        Dim UserLocation(0) As UserDynamicLocationProp
        Try
            Dim command As New SqlCommand("", Connection)                              'Query in ""
            Connection.Open()
            Dim i As Integer = 1
            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                ReDim Preserve UserLocation(UserLocation.Length + 1)
                UserLocation(i).NET_ID = reader.Item("NET_ID").ToString()
                UserLocation(i).User_Latitude = reader.Item("Latitude").ToString()
                UserLocation(i).User_Longitude = reader.Item("Longitude").ToString()
                UserLocation(i).User_Altitude = reader.Item("Altitude").ToString()
                UserLocation(i).User_TimeStamp = reader.Item("TimeStamp").ToString()
                UserLocation(i).User_Accuracy = reader.Item("Accuracy").ToString()
                UserLocation(i).User_Altitude_Accuracy = reader.Item("Altitude_Accuracy").ToString()
                i = i + 1
            End While
        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        'log.append("exited getOtherUserLocationsFromDB", "info")

        Return UserLocation
    End Function
    Function getAllUserLocationsFromDB(ByVal User As UserDynamicLocationProp) As UserDynamicLocationProp()
        Dim UserLocation(0) As UserDynamicLocationProp
        Try
            Dim cmd As String = ""
            Dim command As New SqlCommand(cmd, Connection)                              'Query in ""
            Connection.Open()
            Dim i As Integer = 0
            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                ReDim Preserve UserLocation(i + 1)
                Dim userloc As New UserDynamicLocationProp
                userloc.NET_ID = reader.Item("NET_ID").ToString()
                userloc.User_Latitude = reader.Item("Latitude").ToString()
                userloc.User_Longitude = reader.Item("Longitude").ToString()
                userloc.User_Altitude = reader.Item("Altitude").ToString()
                userloc.User_TimeStamp = reader.Item("Time_stamp").ToString()
                userloc.User_Accuracy = reader.Item("Accuracy").ToString()
                userloc.User_Altitude_Accuracy = reader.Item("Alt_Accuracy").ToString()
                ' MsgBox(UserLocation(i).NET_ID)
                UserLocation(i + 1) = userloc
                i = i + 1
            End While
        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try

        Return UserLocation

    End Function

    Function createUserInDB(ByVal UserObj As UserProp, ByVal passcode As String) As Boolean
        Dim count As Integer = 0
        Try
            Dim command As New SqlCommand("", Connection)                              'Query in ""

            Dim command1 As New SqlCommand("", Connection)                              'Query in ""
            Dim command2 As New SqlCommand("", Connection)                              'Query in ""
            Connection.Open()
            count = command.ExecuteNonQuery()
            If count = 1 Then
                count = command1.ExecuteNonQuery()
                If count = 1 Then
                    command2.ExecuteNonQuery()
                End If
            End If

        Catch e As Exception
            reportError(e)
        Finally
            Connection.Close()
        End Try
        'Query
        If count = 1 Then
            Return True
        Else
           End If
 	Return False
    End Function

End Class

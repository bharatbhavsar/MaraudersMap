Public Class UserController
    Function UpdateLocation(ByVal userlocation As UserDynamicLocationProp) As Boolean
        Dim dataobj As New DAL_Functions
        Return dataobj.updateLocationToDB(userlocation)

    End Function
    Function getOtherUserLocations(ByVal otheruserlocations As UserProp()) As UserDynamicLocationProp()
        Dim dataobj As New DAL_Functions
        Return dataobj.getOtherUserLocationsFromDB(otheruserlocations) 'Check for variable

    End Function

    Function getAllUserLocations(ByVal userobj As UserDynamicLocationProp) As UserDynamicLocationProp()
        Dim dataobj As New DAL_Functions
        Return dataobj.getAllUserLocationsFromDB(userobj) 'check for function (Changed function from All to Other)

    End Function

    Function search(ByVal text As String) As Object
        Dim dataobj As New DAL_Functions
        Dim searchresults(2) As Object
        searchresults(1) = dataobj.getPlaceDetailsFromDB(text)
        searchresults(2) = dataobj.getUserFromDB(text)

        Return searchresults
    End Function
    Function getUser(ByVal userobj As UserProp) As UserProp
        Dim dataobj As New DAL_Functions
        Return dataobj.getUserByIDFromDB(userobj.NET_ID)
    End Function

    Function getLocation(ByVal id As String) As UserDynamicLocationProp
        Dim dataobj As New DAL_Functions
        Return dataobj.getLocation(id)
    End Function
End Class

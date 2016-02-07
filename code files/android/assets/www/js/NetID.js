function getNetID()
{

//window.localStorage.clear();
var netid=window.localStorage.getItem("netid");
if(netid == null)
window.location="LoginPage.html";
return netid;
}

function search()
{
var searchtext=document.getElementById("searchtext").value;

var att = "http://utdmaraudersmap.somee.com/maraudersmapweb/Search.aspx?type=search&searchtext="+searchtext;
alert(att);    
   var xmlhttp;
  
if (window.XMLHttpRequest)
  {// code for IE7+, Firefox, Chrome, Opera, Safari
  xmlhttp=new XMLHttpRequest();
  }
else
  {// code for IE6, IE5
  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }
xmlhttp.onreadystatechange=function()
  {
    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
  
      var searchresult =(xmlhttp.responseText);

       searchresult= searchresult.substring(searchresult.indexOf("["),searchresult.lastIndexOf("]")+1);
		    searchresult= JSON.parse(searchresult);//alert(searchresult);
       showresults(searchresult);
    }
    else {
        alert("not ready"+xmlhttp.readyState+xmlhttp.status);
    }
}
xmlhttp.open("GET", att, false);
xmlhttp.send();



}
function searchtext(searchtext)
{
 
var att = "http://utdmaraudersmap.somee.com/maraudersmapweb/Search.aspx?type=search&searchtext="+searchtext;
alert(att);    
   var xmlhttp;
  
if (window.XMLHttpRequest)
  {// code for IE7+, Firefox, Chrome, Opera, Safari
  xmlhttp=new XMLHttpRequest();
  }
else
  {// code for IE6, IE5
  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }
xmlhttp.onreadystatechange=function()
  {
    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
  
      var searchresult =(xmlhttp.responseText);

       searchresult= searchresult.substring(searchresult.indexOf("["),searchresult.lastIndexOf("]")+1);
		    searchresult= JSON.parse(searchresult);//alert(searchresult);
       showresults(searchresult);
    }
    else {
        alert("not ready"+xmlhttp.readyState+xmlhttp.status);
    }
}
xmlhttp.open("GET", att, false);
xmlhttp.send();



}
function minimize()
{
	 $("#searchresultbox").animate({top:"80%"});
	 $("#searchresultbox").animate({height:"20%"});
	document.getElementById("minmax").innerHTML="<input type=\"button\" value=\"Maximize\" onclick=\"maximize()\"></input>";
}
function maximize()
{
	$("#searchresultbox").animate({height:"60%"});
	$("#searchresultbox").animate({top:"40%"});
	document.getElementById("minmax").innerHTML="<input type=\"button\" value=\"Minimize\" onclick=\"minimize()\"></input>";
}
var searchmarker=new google.maps.Marker({
    shadow: null,
    zIndex: 9,
  });

function showMarker(id)
{
var att = "http://utdmaraudersmap.somee.com/maraudersmapweb/Search.aspx?type=location&id="+id;
alert(att);    
   var xmlhttp;
  
if (window.XMLHttpRequest)
  {// code for IE7+, Firefox, Chrome, Opera, Safari
  xmlhttp=new XMLHttpRequest();
  }
else
  {// code for IE6, IE5
  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }
xmlhttp.onreadystatechange=function()
  {
    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
  
      var searchresult =(xmlhttp.responseText);

       	searchresult= searchresult.substring(searchresult.indexOf("["),searchresult.lastIndexOf("]")+1);
	searchresult= JSON.parse(searchresult);alert(searchresult[0].latitude+" "+ searchresult[0].longitude);

	var userposition = new google.maps.LatLng( searchresult[0].latitude, searchresult[0].longitude);
	searchmarker.setPosition(userposition);
	
	searchmarker.setMap(map);
	minimize();
	map.setCenter(userposition);

    }
    else {
        alert("not ready"+xmlhttp.readyState+xmlhttp.status);
    }
}
xmlhttp.open("GET", att, false);
xmlhttp.send();
	
}
function showresults(searchresult)
{
	var searchtext="<div style=\"display:block;position:relative; float:left; padding:2%;\" id=\"minmax\"><input type=\"button\" value=\"Minimize\" onclick=\"minimize()\"></input></div>";
	for(var i=0; i<searchresult.length; i++)
	{
		searchtext+="<div onclick=\"showMarker(this.id)\" style=\"display:block;position:relative; float:left; margin-left:3%; margin-right:3%; margin-bottom:3%; margin-top:3%;background-color:#FFFFFF; width:85%; padding:5%; \" ";
		if(searchresult[i].type=="user")
		{
			searchtext+="id=\""+searchresult[i].netid+"\"><img src=\"icons/person.jpg\" width=\"7%\" height=\"7%\"/><span style=\"font-size:2em\">"+searchresult[i].fname+" "+searchresult[i].lname+"</span><br>";
			searchtext+="<span style=\"font-size:1em\">"+searchresult[i].netid+"@utdallas.edu</span><br>";
			if(searchresult[i].mobile!="NULL")searchtext+="<span style=\"font-size:1em\">"+searchresult[i].mobile+"</span><br>";
			if(searchresult[i].facebook!="NULL")searchtext+="<a href=\"fb://profile/"+searchresult[i].facebook+"\"><img src=\"icons/facebook.png\" width=\"7%\" height=\"7%\"/></a><br>";
		//	if(searchresult[i].linkedin!="NULL")searchtext+="<a href=\""+searchresult[i].linkedin+"\"><img src=\"icons/linkedin.jpg\" width=\"7%\" height=\"7%\"/></a><br>";
		
		}
		else
		{
			
			searchtext+="id=\""+searchresult[i].placeid+"\"><img src=\"icons/office.png\" width=\"7%\" height=\"7%\"/>";
			searchtext+="<span style=\"font-size:2em\">"+searchresult[i].placename+"</span><br>";
			if(searchresult[i].placeemail!="NULL")searchtext+="<span style=\"font-size:1em\">"+searchresult[i].placeemail+"</span><br>";
			if(searchresult[i].placephone!="NULL")searchtext+="<span style=\"font-size:1em\">"+searchresult[i].placephone+"</span><br>";
			if(searchresult[i].placehours!="NULL")searchtext+="<span style=\"font-size:1em\">"+searchresult[i].placehours+"</span>";
			
		}
			searchtext+="</div>";
	}	
	
	document.getElementById("searchresultbox").innerHTML=searchtext;//alert(searchtext);
		
	  $("#searchresultbox").animate({height:"60%"});
		 $("#searchresultbox").animate({top:"40%"});
//alert(document.getElementById("searchresultbox").innerHTML);
}

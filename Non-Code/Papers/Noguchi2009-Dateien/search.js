<!--
function site_search(f){
	if(f.sitesearch[1].checked == true){
		f.term1.value = f.q.value;
		f.action = "http://search.japantimes.co.jp/cgi-bin/JTsearch5.cgi";
		f.submit;
	}
}

function chkEnterKey(f){
	if(f.sitesearch[1].checked == true){
		f.term1.value = f.q.value;
		f.action = "http://search.japantimes.co.jp/cgi-bin/JTsearch5.cgi";
		f.submit;
	}else{
		//‘—M‰Â 
		return true; 
	}
}
function clearSearchKeyword() {
	if (document.searchform.q.value == "Search our site"){
		document.searchform.q.value = "";
	}
	document.searchform.q.className = '.input-black';
}
//-->

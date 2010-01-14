function loadHtml(fileName,flg){
	if(!flg){
		var radioList = document.getElementsByName("poll_sel");
		for (i=0; i<radioList.length; i++){
			if (document.poll_form.elements[i].checked){
				n = i + 1;
			}
		}
		var postData = "poll_sel=pl_ans" + n;
	}else{
		var postData = "tab_flg=" + flg;
	}
	cbFunc = {
		success:function(httpObj){
			YAHOO.util.Dom.get("poll").innerHTML = httpObj.responseText;
		},
		failure:function(httpObj){
			YAHOO.util.Dom.get("poll").innerHTML = "error poll system Ajax";
		}
	}
	YAHOO.util.Connect.asyncRequest("post",fileName,cbFunc,postData);
}

function loadPoll(cli){
	var postData = "poll_sel=" + cli;

	cbFunc = {
		success:function(httpObj){
			YAHOO.util.Dom.get("poll").innerHTML = httpObj.responseText;
		},
		failure:function(httpObj){
			YAHOO.util.Dom.get("poll").innerHTML = "error poll system Ajax";
		}
	}
	YAHOO.util.Connect.asyncRequest("post","/_scripts/poll_more.php",cbFunc,postData);
}

function loadPollList(line){
	var postData = "poll_list=" + line;

	cbFunc = {
		success:function(httpObj){
			YAHOO.util.Dom.get("poll_list").innerHTML = httpObj.responseText;
		},
		failure:function(httpObj){
			YAHOO.util.Dom.get("poll_list").innerHTML = "error poll system Ajax";
		}
	}
	YAHOO.util.Connect.asyncRequest("post","/_scripts/poll_list.php",cbFunc,postData);
}
function getPollHeight(search_poll_frame,search_iframe){
	if(document.height){
		document.getElementById(search_poll_frame).style.height = parent.frames[search_iframe].document.height + 20 + "px";
	}else{
		document.getElementById(search_poll_frame).style.height = parent.frames[search_iframe].document.body.scrollHeight + 20 + "px";
	}
}

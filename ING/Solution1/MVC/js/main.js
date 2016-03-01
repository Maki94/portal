$(function () {
	
	var send =  true;

    //if table content overflows 

    $(".td-subcontent").each(function(){
        if ($(this).height() > $(".table-data-content").height()) {
            $(this).parent(".table-data-content").find("span.plus").show();	
        }
    });

      $("body").on("click", ".plus", function(){
                $(this).closest(".table-data-content").toggleClass("toggleOverflowTable");
				togglePlusMinus($(this).parent(".table-data-content").find("span.plus"));
				
            });
			
	function togglePlusMinus(obj){		
				if($(obj).html().trim() == "+")
					$(obj).html("-");
				else
					$(obj).html("+");
	}		

	
	     
	//create-edit holiday
  
    $("#add-holiday-btn").click(function(){
		resetInput();
		showDialogCreate();
        $(".dialog").dialog('open');
    });
	
	$(".edit-holiday-btn").click(function(){
		var x = $(this).parent().parent();
			
		$(".holidayIDInput").val( x.find('.hiddenID').val());
		$(".nameInput").val( x.find('.nameInfo').html().trim());		
	
		$(".sameInput").prop('checked', x.find('.sameInfo').find('.check-box').prop('checked'));
		
		var start =  x.find('.startDateInfo').html().trim();		
		var end = x.find('.endDateInfo').html().trim();
				
		$(".startDateInput").val( start);
		$(".endDateInput").val( end);			
		
		showDialogEdit();
		
        $("#dialogEdit").dialog('open');
    }); 	

	//create-edit team    
	
	var editTeam = false;

	$("#create-team-btn").click(function () {
		showDialogCreate();
		editTeam = false;
		$('.dialog').dialog('open');
	    positionDialogAtTop(300); 
	});

	
	 $(".edit-team-btn").click(function () {  
		var x = $(this).parent().parent();
		$(".teamIDInput").val( x.find('.hiddenID').val());
		$(".nameInput").val(x.find(".teamName").html().trim())
		
		 showDialogEdit();			
		 editTeam = true;
		 $('#dialogEdit').dialog('open');
		 ajustDialogPosition(this);	
	});
	
	// create/edit request
	
	$("#sendRequst-btn").click(function () {	
		resetInput();
		$('.dialog').dialog('open');
		positionDialogAtTop(300); 
		showDialogCheck();
		send = true;
	});
	
	 $(".edit-leave-btn").click(function () {  
		var x = $(this).parent().parent().parent();
		
		$(".leaveIDInput").val( x.find('.hiddenID').val());
		$(".typeInput").val(parseInt(x.find('.typeID').val().trim()));
		$(".paidInput").val(parseInt(x.find('.paidID').val().trim()));		
		$(".startDateInput").val(x.find('.startDateInfo').html().trim());
		$(".endDateInput").val(x.find('.endDateInfo').html().trim());
		$(".commentInput").val( x.find('.comment').find('.commentInfo').html().trim());
		send = false;
		showDialogCheck();
		$('#dialogEdit').dialog('open');
		ajustDialogPosition(this);
	});
	
	// add employee to team
	
	$(".addEmpToTeam-btn").click(function (){
		
		var x = $(this).parent().parent();
		
		var teamIdDetailsPageTeamTable = x.find('.teamID').val();			
		var teamIDTeamsIndexPage = x.find('.hiddenID').val();		
		var teamFilterEmployeesIndexPage = $('.teamFilter').val();
			
		if(teamIdDetailsPageTeamTable != null)
		{
			$('.selectedTeam').val(teamIdDetailsPageTeamTable);
			$('#selectTeams').val(teamIdDetailsPageTeamTable);			
		}			
		else if(teamIDTeamsIndexPage !=  null)
		{
			$('.selectedTeam').val(teamIDTeamsIndexPage);
			$('#selectTeams').val(teamIDTeamsIndexPage);
		}	
		else if(teamFilterEmployeesIndexPage !=  null)
		{
			$('.selectedTeam').val(teamFilterEmployeesIndexPage);
			$('#selectTeams').val(teamFilterEmployeesIndexPage);
		}		
		
		$('#selectEmployees').val("");
		$('#dialogAddEmployeeToTeam').dialog('open');
		if(teamIdDetailsPageTeamTable != null)
		{
			positionDialogAboveClickPosition(this);
		}
	});	
	
	
	$(".addTeamToEmp-btn").click(function (){
		var x = $(this).parent().parent();
		var employeeIDDetailsPage = $('.hiddenID').val();		
		var employeeFilterTeamsIndexPage = $('.employeeFilter').val();
		
		if(employeeFilterTeamsIndexPage !=  null)
		{
			$('.selectedEmployee').val(employeeFilterTeamsIndexPage);
			$('#selectEmployees').val(employeeFilterTeamsIndexPage);
		}
		else if(employeeIDDetailsPage !=  null)
		{
			$('.selectedEmployee').val(employeeIDDetailsPage);
			$('#selectEmployees').val(employeeIDDetailsPage);
		}
		
		$('#selectTeams').val("");
		$('#dialogAddEmployeeToTeam').dialog('open');
		if(employeeFilterTeamsIndexPage ==  null)
		{
			positionDialogAboveClickPosition(this);
		}
		
	});
	
	// add employee
	
	$(".addEmployee-btn").click(function(){
		resetInput();
		
		$('.dialog').dialog('open');
		$('.password').val('123456');
		positionDialogAtTop(300);		
	});
	
	$("#addEmployee-btn").click(function(e){
		e.preventDefault();
		var btn = this;
		
	$.ajax({
		url: "/Employees/AddEmployee", 
		method: "POST",
		data: {
			Role: $(this).parent().find('#Role').val(),
			FirstName: $(this).parent().find('input[name=FirstName]').val(),
			LastName: $(this).parent().find('input[name=LastName]').val(),
			Username: $(this).parent().find('input[name=Username]').val(),
			Password: $(this).parent().find('input[name=Password]').val(),
			DateOfBirth: $(this).parent().find('input[name=DateOfBirth]').val(),
			ContractStart: $(this).parent().find('input[name=ContractStart]').val(),
			ContractEnd: $(this).parent().find('input[name=ContractEnd]').val(),
			Status: $(this).parent().find('input[name=Status]').val()
		},
		success: function(res){
			if(res.success)
			{
				document.location.reload();
			} 
			else
			{
			$.each(res.result, function(){
				$(btn).parent().find('span[data-valmsg-for="' + this.fieldName + '"]').html(this.errorList[0]);
			});
			}
        
		}
		
	});
	});
	
	//emp history
	
	 $(".addPdf-btn").click(function () {
		 $('.employeeIDInput').val($('.employeeID').val());
		 var x = $(this).parent().parent();
		 $('.statusIDInput').val(x.find('.statusID').val());
		 
		$('#dialogEdit').dialog('open');
	 });		
	
	
	 $("#changeStatus-btn").click(function () {
		resetInput();
		$('.dialog').dialog('open');
	});
   
   // close all dialogs

    $('.cancel-button').click(function() {
  		$(".dialog").dialog("close");
		$("#dialogEdit").dialog("close");
		$("#dialogAddPdf").dialog('close');
		$("#dialogAddEmployeeToTeam").dialog("close");	
		editTeam =false;
        return false;
    }); 
   
   // ajust dialog position

	function positionDialogAtTop(distance)
	{
		distance =  distance + 'px';		
		$( ".ui-dialog" ).css("position","absolute");  
		$( ".ui-dialog" ).css("top",distance);
	}
	
	
	function ajustDialogPosition(obj){
		$( ".ui-dialog" ).css("position","absolute");  
		var pos = Math.round($(obj).position().top) + 'px';
		$( ".ui-dialog" ).css("top",pos);		
	} 
	
	function positionDialogAboveClickPosition(obj){
		$( ".ui-dialog" ).css("position","absolute");  
		var top = $(obj).position().top;
		var height = $( ".ui-dialog" ).height();
		var pos  =  Math.round(top - height) + 'px';
		$( ".ui-dialog" ).css("top",pos);
	}

	//dialog pop up
	
   $(".dialog").dialog({
		autoOpen: false,
		show: { effect: "fadeIn", duration: 800 },
		modal: true,
		resizable: false,
		
		open: function () {	
			resetInput();	
			ajustButtonsPosition(this);
			$('.ui-widget-overlay').addClass('custom-overlay');
			
		},
		close: function () {			
			$('.ui-widget-overlay').removeClass('custom-overlay');
		},		
		ajust:function () {				
			ajustButtonsPosition(this);			
		}

	});
	
	$("#dialogEdit").dialog({
		autoOpen: false,
		show: { effect: "fadeIn", duration: 800 },
		modal: true,
		resizable: false,
	   
		open: function () {				
			ajustButtonsPosition(this);					
			$('.ui-widget-overlay').addClass('custom-overlay');
			
		},
		close: function () {
			$('.popupInput').val("");
			$('.ui-widget-overlay').removeClass('custom-overlay');
		},
		ajust:function () {				
			ajustButtonsPosition(this);			
		}

	});
	
	 $("#dialogAddPdf").dialog({
		autoOpen: false,
		show: { effect: "fadeIn", duration: 800 },
		modal: true,
		resizable: false,
		
		open: function () {	
			resetInput();	
			ajustButtonsPosition(this);
			$('.ui-widget-overlay').addClass('custom-overlay');
			
		},
		close: function () {
			
			$('.ui-widget-overlay').removeClass('custom-overlay');
		}

	});	 
	
			
	 $("#dialogAddEmployeeToTeam").dialog({
		autoOpen: false,
		show: { effect: "fadeIn", duration: 800 },
		modal: true,
		resizable: false,
		
		open: function () {	
			resetInput();	
			ajustButtonsPosition(this);			
			$('.ui-widget-overlay').addClass('custom-overlay');
			
		},
		close: function () {			
			$('.ui-widget-overlay').removeClass('custom-overlay');
		}
	});
	
	
	//check request 
	
	$('#checkBtn').click(function(){
		SendRequestPopup();
	});	
	 
	
	function SendRequestPopup(){
	var type = $('#type').val();
	var start = $('#startDate').val();
	var end = $('#endDate').val();
	var comment = $('#comment').val();		

	var url = $('#sendRequestPopupId').val();
	$.post(url, { type:type, startDate: start, endDate: end, comment:comment}, function (res) {
		if(res.errorOccurred)
		{
			var error = res.error;
			$('.checkError').text(error);
			$('.checkPassed').hide();		
			$('.checkError').show();			
		}
		else
		{
			var leaveDuration = res.leaveDuration;
			var ldn = parseInt($('#leaveDaysNum').val().trim());
		
			var ldntext = "You have " + ldn + " days left.";
			var text =   "This leave takes " + leaveDuration + " days." ; 
			var aftertext = "Remaining days off after this leave: " + (ldn - leaveDuration);
			$('#checkBtn').hide();
			$('.checkError').hide();
			$('.ldnText').text(ldntext);	
			$('.durationText').text(text);
			$('.afterRequestText').text(aftertext);				
			$('.checkPassed').show();	
			
			if(send){
				showDialogCreate();
			}
			else
			{
				showDialogEdit();
			}
			
		}
		ajustButtonsPosition($("#dialogEdit"));
		
		href = '#';
	});
	
	
	return false;

	};	
	
	//reset check messages and ajust buttons on input change
	
	$(".dialogContainer :input[type='text'],.dialogContainer :input[type = 'datetime']").change(function(){
			$('.checkPassed').hide();
			$('.checkError').hide();
			$('#checkBtn').show();
			ajustButtonsPosition($("#dialogEdit"));		
	})
	
	function hideCheckMessages(){
		$('.checkPassed').hide();
		$('.checkError').hide();
	}
	
	function showDialogCheck(){
		hideCheckMessages();
		$(".diagramEdit").hide();
		$(".diagramCreate").hide();
		$(".diagramCheck").show();
	}		
	
	function showDialogCreate(){
		$(".diagramEdit").hide();
		$(".diagramCreate").show();	
		$(".diagramEdit").prop('disabled',true);
		$(".diagramCreate").prop('disabled',false);
	};

	function showDialogEdit(){
		$(".diagramCreate").hide();
		$(".diagramEdit").show();	
		$(".diagramCreate").prop('disabled',true)
		$(".diagramEdit").prop('disabled',false);
	};
	
	$(".dialogContainer").keypress(function(e){			
		if(e.keyCode==13 && editTeam){
        $(".diagramEdit").click();  
    }});

	
	
	// ajust dialog's buttons postion
	
	function ajustButtonsPosition(obj)	{
		var a = $(obj).dialog("open").position();	
		var c = $(obj).dialog("open").height();		
		var b = a.top + c;
		b = Math.round(b) - 30;
		$('.bottom-button').css({top: b+'px'});
	}
   

   // reset all inputs in dialog
   
    function resetInput(){
		$(".dialogContainer :input[type='text'], :input[type = 'datetime']").val("");	
		$('#type').val('0');
		$('#paid').val('1');
	};
  
  
	//date picker
	
	$(".datePicker").datepicker({		
		changeYear:true,
		yearRange: "1950:2050",
        dateFormat: "dd-MM-yy",
        showOtherMonths: true,
		firstDay: 1,
        dayNamesMin: [ 'S','M', 'T', 'W', 'T', 'F', 'S'],
        monthNames: ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"]
		

    }).on("change", function (e) {
        var curDate = $(this).datepicker("getDate");
        $(this).datepicker("setDate", curDate);
		
		setStartDate(curDate);
		setEndDate(curDate);
		setStartDateEdit(curDate);
		setEndDateEdit(curDate);
		
        
		$(this).datepicker("hide");	
        
		 
    });
	
	function setStartDate(curDate){
		if($("#startDate").datepicker("getDate") > curDate || $("#startDate").datepicker("getDate") ==  null)
		{
			$("#startDate").datepicker("setDate", curDate);
		}	
	};
	
	function setEndDate(curDate){
		if($("#endDate").datepicker("getDate") < curDate)
		{
			$("#endDate").datepicker("setDate", curDate);
		}
	}
	
	function setStartDateEdit(curDate){
		if($("#startDateEdit").datepicker("getDate") > curDate || $("#startDateEdit").datepicker("getDate") ==  null)
		{
			$("#startDateEdit").datepicker("setDate", curDate);
		}	
	};
	
	function setEndDateEdit(curDate){
		if($("#endDateEdit").datepicker("getDate") < curDate)
		{
			$("#endDateEdit").datepicker("setDate", curDate);
		}
	}    

   


});
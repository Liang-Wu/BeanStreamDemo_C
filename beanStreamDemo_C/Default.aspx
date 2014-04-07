<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="beanStreamDemo_C._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
  <head>
     	<script type="text/javascript" src="https://www.beanstream.com/scripts/tokenization/jquery-1.8.0.min.js"></script>
     	<script type="text/javascript" src="https://www.beanstream.com/scripts/tokenization/legato-1.1.min.js"></script>
     	<!--[if IE]>
     	<script type="text/javascript" src="https://www.beanstream.com/scripts/tokenization/json2.min.js"></script>
     	<![endif]-->
	<script type="text/javascript">
	    $(document).ready(function () {
	        $("#submitButton").click(function () {
	            // Prevent repeated clicks
	            $("#submitButton").attr("disabled", "disabled");

	            getLegato(function (legato) {
	                if (legato.success) {
	                    $("#frmPayment").append($('<input type="hidden" name="singleUseToken"/>').val(legato.token));
	                    $("#hf_isSubmit").val("true");
	                    $("#frmPayment").submit();
	                    //__doPostBack($("#frmPayment"), '');
	                } else {
	                    //Use the message property to communicate the error
	                    alert(legato.message);
	                    $("#submitButton").removeAttr("disabled");
	                    $("#hf_isSubmit").val("false");
	                }
	            });

	            // Prevent default form submit
	            return false;
	        });
	    });
    </script>
  </head>
  <body>
    <h1>Payment Form</h1>
    <div>
      <form action="../Default.aspx" method="POST" id="frmPayment">
          <input id="hf_isSubmit" name="hf_isSubmit" type="hidden" value="false"/>
        <!-- Do not add "name" attributes to these input controls; otherwise, they will be exposed to the server. If so, you will be non-PCI compliant.-->
        <p>
          <label>Card Number</label>
          <input type="text" id="trnCardNumber" size="20" autocomplete="off" />
        </p>
        <p>
          <label>Card Expiry Month</label>
          <input type="text" id="trnExpMonth" size="2" autocomplete="off"/>
        </p>
        <p>
          <label>Card Expiry Year</label>
          <input type="text" id="trnExpYear" size="2" autocomplete="off"/>
        </p>
        <p>   
          <label>Cvd</label>
          <input type="text" id="trnCardCvd" size="3" autocomplete="off"/>
        </p>  
        <!-- Other input controls -->
        <input type="submit" id="submitButton"/>
      </form>
    </div>    
  </body>
</html>


$(document).ready(function () {
    getcounties()

    getCustomer()


    $("#btnSubmit").click(function () {
       AddCustomer()
    })

    $("#ddlCountry").change(function () {
        emptyDropdown("ddlState")
        emptyDropdown("ddlCity")
        var cid = $("#ddlCountry").val();
        getstates(cid)
    })

    $("#ddlState").change(function () {
        var cid = $("#ddlState").val();
        getcities(cid)
    })



   
})


function emptyDropdown(id) {
    $("#"+id).html("<option value=''>Select</option>")
}

function getcounties() {
    $.ajax({
        url: "/Customer/GetCountry",
        type: "GET",
        async: false,
        success: function (response) {
            // console.log(response)

            var ddl = "<option value=''>Select</option>"
            response.forEach((item, index) => {
                ddl += "<option  value=" + item.id + ">" + item.name + "</option>"
            })
            $("#ddlCountry").html(ddl)
        }
    })
   
}

function getstates(id) {
    $.ajax({
        url: "/Customer/GetState",
        type: "GET",
        async: false,
        data: {"id":id},
        success: function (response) {
            // console.log(response)

            var ddl = "<option value=''>Select</option>"
            response.forEach((item, index) => {
                ddl += "<option  value=" + item.id + ">" + item.name + "</option>"
            })
            $("#ddlState").html(ddl)
        }
    })
}

function getcities(id) {
    $.ajax({
        url: "/Customer/GetCity",
        type: "GET",
        async: false,
        data: {"id":id},
        success: function (response) {
            // console.log(response)

            var ddl = "<option value=''>Select</option>"
            response.forEach((item, index) => {
                ddl += "<option  value=" + item.id + ">" + item.name + "</option>"
            })
            $("#ddlCity").html(ddl)
        }
    })
}

function getCustomer() {
    $.get("/Customer/GetCustomers", function (response) {

      //  console.log(response)

        $("#tbl_customer").DataTable(
        {
            data: response,
                columns: [
                    { "data": "id" },
                    { "data": "name" },
                    { "data": "email" },

                    { "data": "mobile" },
                    { "data": "gender" },
                    { "data": "country" },
                    { "data": "state" },
                    { "data": "city" },
                    
                    {
                        "data": "id",class:"text-center", render: function (id) {
                            var link = "<a onclick='DeleteRecord("+id+")'><i class='fa fa-trash'></i></a>"

                                link += "<a onclick='EditRecord("+id+")'><i class='fa fa-edit'></i></a>"


                            return link
                        }
                        
                        
                        }

                    

                    ]

        }
            )
        
    })
}



function DeleteRecord(id) {
    if (confirm("Are you sure to delete the record") == false) {
       return false
    }
    $.get("/Customer/DeleteCustomer", { "id": id }, function (response) {
         console.log(response)
        if (response.ok) {
            alert(response.message)
            setTimeout(function () {
                window.location.reload()
            }, 6000)
        }
        else {
            alert(response.message)
        }
    })
   // alert(id)

}

function EditRecord(id) {
    $("#exampleModal").modal("show")
    $("#btnSubmit").val("Update")

    $.get("/Customer/GetCustomer", { "id": id }, function (response) {
        console.log(response)

        $("#txtId").val(response.id)
        $("#txtName").val(response.name)
        $("#txtEmail").val(response.email)
        $("#ddlGender").val(response.gender)
        $("#txtmobile").val(response.mobile)
        $("#ddlCountry").val(response.country)
        getstates(response.country)
        $("#ddlState").val(response.state)
        getcities($("#ddlState").val())
        $("#ddlCity").val(response.city)

    })
}

function AddCustomer() {

    var customer = {
        "name": $("#txtName").val(),
        "email": $("#txtEmail").val(),
        "mobile": $("#txtmobile").val(),
        "gender": $("#ddlGender").val(),
        "country": $("#ddlCountry").val(),
        "state": $("#ddlState").val(),
        "city": $("#ddlCity").val()


    }
    //console.log(customer)
    if ($("#btnSubmit").val() == "Submit"  &&  $("#txtId").val() == "") {
        $.post("/Customer/CreateCustomer", customer, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).addClass("text-success")

                setTimeout(function () {
                    window.location.reload()
                }, 6000)
            } else {
                $("#msg").html(response.message).addClass("text-danger")


            }
        })
    }
    else {
        customer.id = $("#txtId").val();
        $.post("/Customer/UpdateCustomer", customer, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).addClass("text-success")
                setTimeout(function () {
                    window.location.reload()
                }, 6000)
            } else {
                $("#msg").html(response.message).addClass("text-danger")


            }
        })
    }
}
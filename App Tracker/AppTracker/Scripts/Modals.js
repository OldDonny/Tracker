$('.AddDepartment').on('click', function () {
    $("#AddDepartmentForm").dialog({
        autoOpen: false,
        position: { my: "center", at: "top+350", of: window },
        width: 1000,
        resizable: false,
        title: 'Add Department',
        modal: true,
        open: function () {
            $(this).load('@Url.Action("CreateView", "Department")');


        },
        buttons: {
            "Add Department": function () {
                addDepartmentInfo();
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
    return false;
});
function addDepartmentInfo() {
    $.ajax({
        url: '@Url.Action("AddDepartment", "Department")',
        type: 'POST',
        data: $("#myForm").serialize(),
        success: function (data) {
            if (data) {
                $(':input', '#myForm')
                  .not(':button, :submit, :reset, :hidden')
                  .val('')
                  .removeAttr('checked')
                  .removeAttr('selected');
            }
        }
    });
}
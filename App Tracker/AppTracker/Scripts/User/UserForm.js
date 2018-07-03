var existingApps = [];
var existingUserAppRoles = [];
var grabUserAppsAndRoles = [];
var apps = [];
var appRoles = [];

$(document).ready(function () {

    const appsUrl = $("#Appsloader").data("request-url");
    const appRolesUrl = $("#AppRolesloader").data("request-url");

    const grabUserAppsAndRolesUrl = $("#GrabUserAppsAndRoles").data("request-url");
    const id = $("#blazerId").val();

    //List of all Apps in Applist
    $.ajax({
        type: "GET",
        url: appsUrl,
        success: function (data) {
            apps = data.items;
            console.log(apps, "total apps");
        },
        error: function () {
            alert("there was an error loading apps from ajax call");
        }
    });

    //List of All Roles
    $.ajax({
        type: "GET",
        url: appRolesUrl,
        success: function (data) {
        appRoles = data.items;
        console.log(appRoles, "total roles");           
        },
        error: function () {
            alert("there was an error loading appRoles from ajax call");
        }
    });

    $.ajax({
        type: "GET",
        url: grabUserAppsAndRolesUrl,
        data: { id: id },
        success: function (data) {
            checkIfExistingApps(data.items);
            grabUserAppsAndRoles = data.items;
            console.log(grabUserAppsAndRoles, "grabUserAppsAndRoles");
        },
        error: function () {
            console.log("there was an error loading Existing Apps from ajax call");
        }
    });

    //Select2
    const employeesSearchUrl = $("#EmployeeSelect2").data("request-url");

    $("#test").select2({
        minimumInputLength: 4,
        width: "93%",
        ajax: {
            url: employeesSearchUrl,
            type: "POST",
            dataType: "json",
            data: function (term) {
                return {
                    term: term
                };
            },
            results: function (data) {
                return { results: data.items };
            }
        },
        initSelection: function (item, callback) {
            const text = $(item).val();
            const data = { id: text, text: text };
            callback(data);
        }
    });

    //UserApp row functions
    const userAppRow = {
        add: function (row) {
            //Only lets the user add UserAppRows depending of the number of Apps they are Admin on
            if (numberOfRows <= apps.length) {
                $("#btn-container").append(row);
            }
        },
        remove: function (numberOfRows, data)  {
            const userAppContainerId = `#userAppContainer${data.index}`;

            $(userAppContainerId).remove();

            const leftAppRows = $(".user-app-row");

            $(leftAppRows).each(function () {
                const divDataIndex = $(this).attr("data-index");

                const newIndex = divDataIndex - 1;
                const newDivId = `userAppContainer${newIndex}`;
                const newAppsSelectName = `Apps[${newIndex}].Id`;
                const newAppsSelectId = `Apps_${newIndex}_Id`;
                const newAppRolesSelectName = `AppRoles[${newIndex}].Id`;
                const newAppRolesSelectId = `AppsRoles_${newIndex}_Id`;
                const newRemoveAppDataIndex = `${newIndex}`;
                const newRemoveAppId = `deleteUserApp${newIndex}`;

                if (divDataIndex > data.index) {
                    $(this).attr("id", newDivId);
                    $(this).attr("data-index", newIndex);

                    $(this).find(".apps-select").attr("name", newAppsSelectName);
                    $(this).find(".apps-select").attr("id", newAppsSelectId);

                    $(this).find(".approles-select").attr("name", newAppRolesSelectName);
                    $(this).find(".approles-select").attr("id", newAppRolesSelectId);

                    $(this).find(".remove-app").attr("data-index", newRemoveAppDataIndex);
                    $(this).find(".remove-app").attr("id", newRemoveAppId);
                }
            });
        }
    };

    var numberOfRows = 0;  

    $(document).ajaxStop(function () {
        if (grabUserAppsAndRoles.length > 0) {
            creatExistingUserAppRows();
        }
        
        //On dropdownlist change logic
        $("#btn-container").on("change",
            ".apps-select",
            function() {
                console.log("here is the array logic");

            });

    });

    $("#addUserApp").on("click", (function () {

        numberOfRows++;

        var optionsForApps = "";
        var optionsForAppRoles = "";

        for (var i = 0; i < apps.length; i++) {
            optionsForApps += `<option value="${apps[i].id}">${apps[i].Text}</option>`;
        }

        for (var i = 0; i < appRoles.length; i++) {
            optionsForAppRoles += `<option value="${appRoles[i].id}">${appRoles[i].Text}</option>`;
        }

        var divUserAppRowId = "";
        var divUserAppRowDataIndex = "";
        var selectAppsName = "";
        var selectAppsId = "";
        var selectAppRolesName = "";
        var selectAppRolesId = "";
        var aIndex = "";

        if (numberOfRows === 1) {
            //div container of userapp
            divUserAppRowId = `userAppContainer${0}`;
            divUserAppRowDataIndex = `${0}`;

            //a#deleteUserApp
            aIndex = `${0}`;

            //select for apps
            selectAppsName = `Apps[${0}].Id`;
            selectAppsId = `Apps_${0}_Id`;

            //select for appRoles
            selectAppRolesName = `AppRoles[${0}].Id`;
            selectAppRolesId = `AppRoles_${0}_Id`;


        }
        else if (numberOfRows > 1) {
            //div container of userapp
            divUserAppRowId = `userAppContainer${numberOfRows - 1}`;
            divUserAppRowDataIndex = `${numberOfRows - 1}`;

            //a#deleteUserApp
            aIndex = `${numberOfRows - 1}`;

            //select for apps
            selectAppsName = `Apps[${numberOfRows - 1}].Id`;
            selectAppsId = `Apps_${numberOfRows - 1}_Id`;

            //select for roles
            selectAppRolesName = `AppRoles[${numberOfRows - 1}].Id`;
            selectAppRolesId = `AppRoles_${numberOfRows - 1}_Id`;
        }

        const newUserAppRow = `
                <div id = "${divUserAppRowId}" class ="row user-app-row" style="margin-bottom: 6vh" data-index="${divUserAppRowDataIndex}">
                    <div class="form-group">
                        <div class ="col-lg-5">
                            <label>App Name</label>
                            <select name="${selectAppsName}" id="${selectAppsId}"class ="form-control apps-select">
                                ${optionsForApps}
                            </select>
                        </div>
                        <div class="col-lg-5">
                            <label>Role</label>
                            <select name="${selectAppRolesName}" id="${selectAppRolesId}"class ="form-control approles-select">${optionsForAppRoles}
                            </select>
                        </div>
                    </div>
                    <div class="col-lg-1" style="margin-top: 1%">
                        <a id="deleteUserApp${aIndex}" class ="active remove-color remove-app" data-index="${aIndex}">
                            <i class ="fa fa-times" aria-hidden="true"></i>
                            Remove
                        </a>
                    </div>
                </div>
                `;

        userAppRow.add(newUserAppRow);
    }));

    $("#btn-container").delegate("a.remove-color",
        "click",
        function () {
            numberOfRows--;
            const index = $(this).data();
            userAppRow.remove(numberOfRows, index);
        });

    //Ajax Data functions
    function checkIfExistingApps(data) {
        if (data.length > 0) {            
            numberOfRows = data.length;
        }
    }

    function creatExistingUserAppRows() {

        //foreach existing UserApp
        grabUserAppsAndRoles.forEach(item => {
            var optionsForApps = "";
            var optionsForAppRoles = "";

            console.log(item);

            //Options for Apps
            for (var i = 0; i < apps.length; i++) {
                if (item.AppId === apps[i].id) {
                    optionsForApps += `<option value="${item.AppId}" selected>${item.AppName}</option>`; 
                } else {
                    optionsForApps += `<option value="${apps[i].id}">${apps[i].Text}</option>`;
                }
            }
            //Options for AppRoles
            for (var j = 0; j < appRoles.length; j++) {
                if (item.RoleId === appRoles[j].id) {
                    optionsForAppRoles += `<option value="${item.RoleId}" selected>${item.RoleName}</option>`;
                } else {
                    optionsForAppRoles += `<option value="${appRoles[j].id}">${appRoles[j].Text}</option>`;
                }
            }

            var divUserAppRowId = `userAppContainer${item.Id}`;
            var divUserAppRowDataIndex = `${item.Id}`;

            //a#deleteUserApp
            var aIndex = `${item.Id}`;

            //select for apps
            var selectAppsName = `Apps[${item.Id}].Id`;
            var selectAppsId = `Apps_${item.Id}_Id`;

            //select for roles
            var selectAppRolesName = `AppRoles[${item.Id}].Id`;
            var selectAppRolesId = `AppRoles_${item.Id}_Id`;

            const existingUserAppRow = `
                <div id = "${divUserAppRowId}" class ="row user-app-row" style="margin-bottom: 6vh" data-index="${divUserAppRowDataIndex}">
                    <div class="form-group">
                        <div class ="col-lg-5">
                            <label>App Name</label>
                            <select name="${selectAppsName}" id="${selectAppsId}"class ="form-control apps-select">
                                ${optionsForApps}
                            </select>
                        </div>
                        <div class="col-lg-5">
                            <label>Role</label>
                            <select name="${selectAppRolesName}" id="${selectAppRolesId}"class ="form-control approles-select">
                                ${optionsForAppRoles}
                            </select>
                        </div>
                    </div>
                    <div class="col-lg-1" style="margin-top: 1%">
                        <a id="deleteUserApp${aIndex}" class ="active remove-color remove-app" data-index="${aIndex}">
                            <i class ="fa fa-times" aria-hidden="true"></i>
                            Remove
                        </a>
                    </div>
                </div>
                `;
            userAppRow.add(existingUserAppRow);
        });
    }
});




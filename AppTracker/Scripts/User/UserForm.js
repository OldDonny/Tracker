var existingApps = [];
var existingUserAppRoles = [];
var apps = [];
var appRoles = [];

$(document).ready(function () {
    //Apps and AppRoles by BlazerId
    
    const existingAppsUrl = $("#ExistingAppsloader").data("request-url");
    const existingUserAppRolesUrl = $("#ExistingUserAppRolesloader").data("request-url");

    //All Apps and AppRoles
   
    const appsUrl = $("#Appsloader").data("request-url");
    const appRolesUrl = $("#AppRolesloader").data("request-url");
    const id = $("#blazerId").val();

    $.ajax({
        type: "GET",
        url: existingAppsUrl,
        data: { id: id },
        success: function (data) {
            checkIfExistingApps(data.items);
            existingApps = data.items;           
            console.log(existingApps, "existingApps");
        },
        error: function () {
            alert("there was an error loading Existing Apps from ajax call");
        }
    });

    $.ajax({
        type: "GET",
        url: existingUserAppRolesUrl,
        data: { id: id },
        success: function (data) {
            existingUserAppRoles = data.items;
            console.log(existingUserAppRoles, "existingUserAppRoles");
        },
        error: function () {
            alert("there was an error loading Existing UserAppRoles from ajax call");
        }
    });

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
            $("#btn-container").append(row);
        },
        remove: function (numberOfRows, data) {
            const userAppContainerId = `#userAppContainer${data.index}`;

            $(userAppContainerId).remove();
            console.log("number of rows after remove", numberOfRows);

            const leftAppRows = $(".user-app-row");

            console.log(leftAppRows);

            $(leftAppRows).each(function () {
                const divDataIndex = $(this).attr("data-index");
                console.log(divDataIndex);

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
        if (existingApps.length > 0) {
            creatExistingUserAppRows();
        }       
    });

    $("#addUserApp").on("click", (function () {

        numberOfRows++;
        console.log(numberOfRows);

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
        console.log(existingApps, "here");
        for (var j = 0; j < existingApps.length; j++) {

            var optionsForApps = "";
            var optionsForAppRoles = "";

            for (var l = 0; l < apps.length; l++) {
                if (apps[l].id === existingApps[j].id) {
                    optionsForApps += `<option value="${apps[l].id}" selected>${apps[l].Text}</option>`;
                }else
                optionsForApps += `<option value="${apps[l].id}">${apps[l].Text}</option>`;
            }

            for (var k = 0; k < appRoles.length; k++) {
                if (appRoles[k].id === existingUserAppRoles[j].id) {
                    optionsForAppRoles += `<option value="${appRoles[k].id}" selected>${appRoles[k].Text}</option>`;
                }else
                optionsForAppRoles += `<option value="${appRoles[k].id}">${appRoles[k].Text}</option>`;
            }

            var divUserAppRowId = `userAppContainer${j}`;
            var divUserAppRowDataIndex = `${j}`;
            
            //a#deleteUserApp
            var aIndex = `${j}`;

            //select for apps
            var selectAppsName = `Apps[${j}].Id`;
            var selectAppsId = `Apps_${j}_Id`;

            //select for roles
            var selectAppRolesName = `AppRoles[${j}].Id`;
            var selectAppRolesId = `AppRoles_${j}_Id`;

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
            userAppRow.add(existingUserAppRow);
        }
    }
});




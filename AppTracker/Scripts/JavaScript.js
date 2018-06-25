
//Datatables initialize

$(document).ready(function () {
   

    
    $('#mytable1').DataTable(
        {
            
            scrollY: 200,
            scrollCollapse: true,
            paging: true,
            sorting: true
        }
        );


    $('#mytable3').DataTable(
        {

            scrollY: 200,
            scrollCollapse: true,
            paging: true,
            sorting: true
        }
    );

    $('#mytable2').DataTable(
      {

          scrollY: 200,
          scrollCollapse: true,
          paging: true,
          sorting: true
      }
      );
    // Apply a search to the second table for the demo
  
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


//Multiple Select Plugin Initialize

$(function() {
    $('#departments').multiselect({
        includeSelectAllOption: true
});
});
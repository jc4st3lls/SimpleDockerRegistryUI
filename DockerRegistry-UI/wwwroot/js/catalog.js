/// <reference path="jquery.min.js" />



var server = '';
function Show(name, tag) {
    $("#ImgT").text(server + "/" + name + ":" + tag);
    
}


function readyFN() {
    $.getJSON("/v2/Catalog", function (catalog) {
        server = catalog.server;
        var data = catalog.images;
        var items = [];

        $.each(data, function (key, val) {
            var tags = val.tags.join(",");
            var tagslink = [];



            $.each(val.tags, function (key1, val1) {
                var params = "'".concat(val.name).concat("','").concat(val1).concat("'");

                var _aref = "<a href=javascript:Show(".concat(params).concat(")>").concat(val1).concat("</a>");

                tagslink.push(_aref);

            });




            items.push("<li id='" + key + "'>" + val.name + "[" + tagslink + "]" + "</li>");


        });

        $("#ListC").html(items.join(""));
    });
   

}

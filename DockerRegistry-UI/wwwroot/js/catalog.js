/// <reference path="jquery.min.js" />


const server = "dockerhub.althaia.cat";

function Show(name, tag) {
    $("#ImgT").text(server + "/" + name + ":" + tag);
    $("#BTR").on("click", function () {
        var path = "/v2/Catalog/".concat(name).concat("/").concat(tag);

        $.ajax({
            url: path,
            type: 'DELETE',
            success: function (data) {
                var res = data;
            }
        });
       
    });
}


function readyFN() {

    


    $.getJSON("/v2/Catalog", function (data) {
        var items = [];
        $.each(data, function (key, val) {
            var tags = val.tags.join(",");
            var tagslink = [];



            $.each(val.tags, function (key1, val1) {
                var params = "'".concat(val.name).concat("','").concat(val1).concat("'");

                var _aref = "<a href=javascript:Show(".concat(params).concat(")>").concat(val1).concat("</a>");

                tagslink.push(_aref);

            });




            items.push("<li id='" + key + "'>" + val.name + "[" + tagslink + "]" +  "</li>");


        });

        $("#ListC").html(items.join(""));
    });

   

}

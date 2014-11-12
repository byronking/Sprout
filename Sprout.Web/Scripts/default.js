$(document).ready(function () {
    $(document).on('change', '.btn-file :file', function () {
        var input = $(this),
        numFiles = input.get(0).files ? input.get(0).files.length : 1,
        label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);

        $('#txtFileName').val(label);
    });

    $("#txtProjectSummary").keyup(function () {
        var max = 300;
        var len = $(this).val().length;
        //alert('length: ' + len);
        if (len >= max) {
            $("#charNumSummary").text(' you have reached the limit');
        } else {
            var char = max - len;
            $("#charNumSummary").text(char + ' characters left');
        }
    });

    $("#txtProjectDescription").keyup(function () {
        var max = 2000;
        var len = $(this).val().length;
        //alert('length: ' + len);
        if (len >= max) {
            $("#charNumDescription").text(' you have reached the limit');
        } else {
            var char = max - len;
            $("#charNumDescription").text(char + ' characters left');
        }
    });
});
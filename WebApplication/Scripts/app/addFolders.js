function updateMonitor(progress) {

    $("#" + progress.Id).html(progress.Message);
    $('.progress-bar').css("width", progress.Percentage + "%");
}




    
    function addFolder(path, sets) {
        $("#progressBar").show();
        
        $.post(pathToRoot + "dropbox/GetTaskIdAddFolder", {},

            function (progress) {

                $("#monitors").append($("<p id='" + progress.Id + "'/>"));

                // Periodically update monitors
                var intervalId = setInterval(function () {

                    $.post(pathToRoot + "dropbox/ProgressAddFolder", { Id: progress.Id, Message: '' }, function (progressUpdate) {
                        if (progressUpdate.Message == 'completed') {
                            updateMonitor(progressUpdate);
                            clearInterval(intervalId);
                            location.reload();
                        } else {
                            updateMonitor(progressUpdate);
                        }
                    });

                }, 500);


                $.post(pathToRoot + "dropbox/StartAddFolder", { id: progress.Id, path: path, sets: sets }, function (startProgress) {
                    updateMonitor(startProgress);
                });


            }
        );


    }

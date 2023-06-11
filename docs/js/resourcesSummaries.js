window.addEventListener("load", function() {
    var details = document.querySelectorAll("details")
    for (let i = 0; i < details.length; i++) {
        details[i].querySelector("summary").addEventListener("click", function(){
            if (details[i].classList == "closed") {
                details[i].classList = "opened"
            } else {
                details[i].classList = "closed"
            }
        })
    }
})
window.onload = function(){
    var nav = document.querySelector("nav")
    var pageLinks = document.querySelector(".pages").getElementsByTagName("a")
    
    // Surlignage de la page active
    slide(nav)
    for (let i = 0; i < pageLinks.length; i++) {
        if (pageLinks[i].href === document.location.href) {
            pageLinks[i].classList.add("current")
        }        
    }
}

// ---
function slide(elem) {
    elem.style.transition = "transform .7s ease-out";
    elem.style.transform = "translateY(0)"
}
const says = ["Stop that","Stop punching me plz","Hey !! It hurts !","what ?","go away","😴","Zzz.. Hm ?","Zzz", "https://youtu.be/dQw4w9WgXcQ"];

window.addEventListener("load", function(){
    var cards = document.querySelectorAll(".card");
    
    for (let i = 0; i < cards.length; i++) {
        cards[i].addEventListener("click", function() {
            alert(says[Math.floor(Math.random() * says.length)])
        });
    }
});
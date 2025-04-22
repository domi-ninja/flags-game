// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function submitAnswer(event, flagId){
  var textEntered = event.target.value;
  var flagNameElement = event.target.parentElement.querySelector(".flag-name");
  var lastSuggestionEl = event.target.parentElement.querySelector(".flag-detail li");
  var countrySuggestionsEl = event.target.parentElement.querySelector(".country-suggestions"); 
  var solution = flagNameElement.innerText;
  if ( textEntered == solution) {
    flagNameElement.classList.remove('hidden');
    flagNameElement.classList.add("green");
  } else if (lastSuggestionEl && lastSuggestionEl.innerText == solution) {
    console.log()
    flagNameElement.classList.remove('hidden');
    flagNameElement.classList.add("green");
  } else {
    flagNameElement.classList.remove('hidden');
    flagNameElement.classList.add("red");
  }

  event.target.classList.add("hidden");
  if (countrySuggestionsEl) {
    countrySuggestionsEl.classList.add("hidden")
  }


}


var save = {}
function init(){
  var storage_name = "flags_game";
  var storage_text = localStorage.getItem(storage_name);
  if ( !storage_text ) {
  } else {
    save = JSON.parse( storage_text );
  }
}

init()
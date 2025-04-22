// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function submitAnswer(event, flagId){
  var textEntered = event.target.value;
  var flagNameElement = event.target.parentElement.querySelector(".flag-name");
  var lastSuggestionEl = event.target.parentElement.querySelector(".flag-detail li");
  var countrySuggestionsEl = event.target.parentElement.querySelector(".country-suggestions"); 
  var solution = flagNameElement.innerText;
  var correct = false;
  if ( textEntered == solution) {
    flagNameElement.classList.remove('hidden');
    flagNameElement.classList.add("green");
    correct = true;
  } else if (lastSuggestionEl && lastSuggestionEl.innerText == solution) {
    flagNameElement.classList.remove('hidden');
    flagNameElement.classList.add("green");
    correct = true;
  } else {
    flagNameElement.classList.remove('hidden');
    flagNameElement.classList.add("red");
  }

  event.target.classList.add("hidden");
  if (countrySuggestionsEl) {
    countrySuggestionsEl.classList.add("hidden")
  }


  update_save_answer(flagId, correct);
}

function update_save_answer(flagId, correct){
  
  var flag_history = save.country_guesses[flagId]
  if ( !flag_history ) {
    save.country_guesses[flagId] = []
    flag_history = save.country_guesses[flagId]
  }
  flag_history.push({
    d: new Date(), 
    s: correct
  }) 

  update_save()
}


function get_fails(){
  var fails = {}
  Object.keys(save.country_guesses).forEach((cid)=>{
    var cg = save.country_guesses[cid];
    fails[cid] = cg[cg.length-1].s
  })

  return JSON.stringify(fails);
}

var storage_name = "flags_game";
var save = {
  country_guesses:{

  },
};

function update_save(){
  localStorage.setItem(storage_name, JSON.stringify(save));
}

function init(){
  var storage_text = localStorage.getItem(storage_name);
  if ( !storage_text ) {
  } else {
    save = JSON.parse( storage_text );
  }
}

init()


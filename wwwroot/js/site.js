// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var config = {}
function configure(configObj)
{
    Object.keys(configObj).forEach(function (k) {
        config[k] = configObj[k];
    })
}

function playOnType(event, flagId) {
    var textEntered = event.target.value;
    var flagNameElement = event.target.parentElement.querySelector(".flag-name");
    var suggestionListEl = event.target.parentElement.querySelector(".flag-detail .country-suggestions");
    var countrySuggestionsEl = event.target.parentElement.querySelector(".country-suggestions");
    var solution = flagNameElement.innerText;

    if (event.key == 'Enter') {
        submitAnswer(event, flagId);
    } else {
        var textEntered = event.target.value;
        //if (textEntered.length < 3) {
        //  return;
        //}

        var matches = window.flags.filter(f => {
          return f.toLowerCase().indexOf(textEntered.toLowerCase()) != -1;
        });

        matches.sort();
        function rank(match) {
            var quality = 0;
            var fStart = match.substr(0, textEntered.length);
            if (fStart.toLowerCase() === textEntered.toLowerCase()) {
                quality = 10;
            } else {

            }
            return quality;
        }
        matches.sort((m1, m2) => {
            return rank(m2) - rank(m1);
        });

        matches = matches.slice(0, 5);

        if (config.hard) {
            suggestionListEl.classList.add("hidden");
        } else {
            suggestionListEl.classList.remove("hidden");
        }
        suggestionListEl.innerHTML = matches.map(m => `<li>${m}</li>`).join("");

    }
}

function playOnBlur(event) {
    var suggestionListEl = event.target.parentElement.querySelector(".flag-detail .country-suggestions");
    suggestionListEl.innerHTML = "";
    suggestionListEl.classList.add("hidden");
}

function submitAnswer(event, flagId){
  var textEntered = event.target.value;
  if (textEntered.length < 1) {
    return;
  }

  var flagNameElement = event.target.parentElement.querySelector(".flag-name");
  var lastSuggestionEl = event.target.parentElement.querySelector(".flag-detail li");
  var userInputEl = event.target.parentElement.querySelector(".flag-detail .user-input");
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
    userInputEl.innerText = lastSuggestionEl.innerText;
  }

  event.target.classList.add("hidden");
  if (countrySuggestionsEl) {
    countrySuggestionsEl.classList.add("hidden")
  }


    update_save_answer(flagId, correct);

    var allInputs = Array.from(  document.querySelector("#flag-list").querySelectorAll("input") );
    var currentIndex = allInputs.indexOf(event.target);
    var nextInput = allInputs[currentIndex + 1];
    if (nextInput) {
        nextInput.focus();
    }
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

function get_answer_stats() {
    var guesses = {};
    Object.keys(save.country_guesses).forEach(cid => {
        var cg = save.country_guesses[cid];
        var right = cg.reduce((sum, el) => sum + (el.s ? 1 : 0), 0);
        var wrong = cg.reduce((sum, el) => sum + (el.s ? 0 : 1), 0);
        guesses[parseInt(cid)] = {
            right: right,
            wrong: wrong,
        };
    });
    console.log(guesses)
    return JSON.stringify( guesses );
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

    // read json array
    fetch("api/flag/Names")
      .then(response => response.json())
      .then(data => {
        // Process the JSON array here
        window.flags = data;
      })
      .catch(error => {
        console.error("Error fetching JSON array:", error);
      });
}

init()


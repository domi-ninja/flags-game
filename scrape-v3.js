https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes#cite_note-ISO_3166-1

JSON.stringify(Array.from(document.querySelectorAll("tr")).map( tr=> {
  var nameEl = tr.querySelectorAll( "td")[0]
	var sovEl = tr.querySelectorAll( "td")[2]
  var shortEl =  tr.querySelectorAll( "td")[3]

  if (sovEl) {
    if ( sovEl.innerText.indexOf("UN") != -1 ) {
      var link =  tr.querySelectorAll( "td")[0].querySelector("a").href

      return {
        link: link,
        name: nameEl.innerText,
        short: shortEl.innerText
      }
    } else {
      if ( nameEl ) {
        console.log( "Ignored " + nameEl.innerText + ": not UN" ) 
      } else {
        console.log( "Ignored ", tr ) 
      }
    }
  } else {
    if ( nameEl ) {
      console.log( "Ignored " + nameEl.innerText + ": no sovEl" ) 
    } else {
      console.log( "Ignored ", tr ) 
    }
  }

}).filter( x=> x))





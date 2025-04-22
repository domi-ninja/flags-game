Array.from(document.querySelectorAll("table tr")).map( function(tr) {
    var country = tr.querySelector("td:nth-child(2)"); 
    if (!country) return;
    
    var pop = tr.querySelector("td:nth-child(3)"); 
    if (!pop) return;

    var data = {
        country: country.innerText.trim(),
        population: pop.innerText.trim(),
    };

    // var a = tr.querySelector("td:nth-child(2)").querySelector("a");
    // if ( a==null ) return;
    
    // data.text = await fetch(a.href).then(function(response) {
    //     return response.text();
    // });

    // console.log(data.text);

    return data;
})

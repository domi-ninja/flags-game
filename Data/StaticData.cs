using System.Text.Json;

namespace flags_game.Data;

public static class StaticData
{
    
    public static List<Country> LoadCountries(){
        #region countries
        string json = @"
    {
        ""countries"": 
        [

    
            {   ""name"": ""Niue"",
                ""short"": ""NU""
            },
            {   ""name"": ""Western Sahara"",
                ""short"": ""EH""
            },
            {
                ""name"": ""Taiwan"",
                ""short"": ""TW""
            },
            {
                ""name"": ""Laos"",
                ""short"": ""LA""
            },
            {
                ""name"": ""Kosovo"",
                ""short"": ""XK""
            },
            {
                ""name"": ""Afghanistan"",
                ""short"": ""AF""
            },
            {
                ""name"": ""Albania"",
                ""short"": ""AL""
            },
            {
                ""name"": ""Algeria"",
                ""short"": ""DZ""
            },
            {
                ""name"": ""Andorra"",
                ""short"": ""AD""
            },
            {
                ""name"": ""Angola"",
                ""short"": ""AO""
            },
            {
                ""name"": ""Antigua and Barbuda"",
                ""short"": ""AG""
            },
            {
                ""name"": ""Argentina"",
                ""short"": ""AR""
            },
            {
                ""name"": ""Armenia"",
                ""short"": ""AM""
            },
            {
                ""name"": ""Australia "",
                ""short"": ""AU""
            },
            {
                ""name"": ""Austria"",
                ""short"": ""AT""
            },
            {
                ""name"": ""Azerbaijan"",
                ""short"": ""AZ""
            },
            {
                ""name"": ""Bahamas "",
                ""short"": ""BS""
            },
            {
                ""name"": ""Bahrain"",
                ""short"": ""BH""
            },
            {
                ""name"": ""Bangladesh"",
                ""short"": ""BD""
            },
            {
                ""name"": ""Barbados"",
                ""short"": ""BB""
            },
            {
                ""name"": ""Belarus"",
                ""short"": ""BY""
            },
            {
                ""name"": ""Belgium"",
                ""short"": ""BE""
            },
            {
                ""name"": ""Belize"",
                ""short"": ""BZ""
            },
            {
                ""name"": ""Benin"",
                ""short"": ""BJ""
            },
            {
                ""name"": ""Bhutan"",
                ""short"": ""BT""
            },
            {
                ""name"": ""Bolivia"",
                ""short"": ""BO""
            },
            {
                ""name"": ""Bosnia and Herzegovina"",
                ""short"": ""BA""
            },
            {
                ""name"": ""Botswana"",
                ""short"": ""BW""
            },
            {
                ""name"": ""Brazil"",
                ""short"": ""BR""
            },
            {
                ""name"": ""Brunei"",
                ""short"": ""BN""
            },
            {
                ""name"": ""Bulgaria"",
                ""short"": ""BG""
            },
            {
                ""name"": ""Burkina Faso"",
                ""short"": ""BF""
            },
            {
                ""name"": ""Burundi"",
                ""short"": ""BI""
            },
            {
                ""name"": ""Cabo Verde"",
                ""short"": ""CV""
            },
            {
                ""name"": ""Cambodia"",
                ""short"": ""KH""
            },
            {
                ""name"": ""Cameroon"",
                ""short"": ""CM""
            },
            {
                ""name"": ""Canada"",
                ""short"": ""CA""
            },
            {
                ""name"": ""Central African Republic "",
                ""short"": ""CF""
            },
            {
                ""name"": ""Chad"",
                ""short"": ""TD""
            },
            {
                ""name"": ""Chile"",
                ""short"": ""CL""
            },
            {
                ""name"": ""China"",
                ""short"": ""CN""
            },
            {
                ""name"": ""Colombia"",
                ""short"": ""CO""
            },
            {
                ""name"": ""Comoros "",
                ""short"": ""KM""
            },
            {
                ""name"": ""Democratic Republic of the Congo (DRC)"",
                ""short"": ""CD""
            },
            {
                ""name"": ""Republic of the Congo"",
                ""short"": ""CG""
            },
            {
                ""name"": ""Costa Rica"",
                ""short"": ""CR""
            },
            {
                ""name"": ""Côte d'Ivoire "",
                ""short"": ""CI""
            },
            {
                ""name"": ""Croatia"",
                ""short"": ""HR""
            },
            {
                ""name"": ""Cuba"",
                ""short"": ""CU""
            },
            {
                ""name"": ""Cyprus"",
                ""short"": ""CY""
            },
            {
                ""name"": ""Czechia "",
                ""short"": ""CZ""
            },
            {
                ""name"": ""Denmark"",
                ""short"": ""DK""
            },
            {
                ""name"": ""Djibouti"",
                ""short"": ""DJ""
            },
            {
                ""name"": ""Dominica"",
                ""short"": ""DM""
            },
            {
                ""name"": ""Dominican Republic "",
                ""short"": ""DO""
            },
            {
                ""name"": ""Ecuador"",
                ""short"": ""EC""
            },
            {
                ""name"": ""Egypt"",
                ""short"": ""EG""
            },
            {
                ""name"": ""El Salvador"",
                ""short"": ""SV""
            },
            {
                ""name"": ""Equatorial Guinea"",
                ""short"": ""GQ""
            },
            {
                ""name"": ""Eritrea"",
                ""short"": ""ER""
            },
            {
                ""name"": ""Estonia"",
                ""short"": ""EE""
            },
            {
                ""name"": ""Eswatini "",
                ""short"": ""SZ""
            },
            {
                ""name"": ""Ethiopia"",
                ""short"": ""ET""
            },
            {
                ""name"": ""Fiji"",
                ""short"": ""FJ""
            },
            {
                ""name"": ""Finland"",
                ""short"": ""FI""
            },
            {
                ""name"": ""France "",
                ""short"": ""FR""
            },
            {
                ""name"": ""Gabon"",
                ""short"": ""GA""
            },
            {
                ""name"": ""Gambia, The"",
                ""short"": ""GM""
            },
            {
                ""name"": ""Georgia"",
                ""short"": ""GE""
            },
            {
                ""name"": ""Germany"",
                ""short"": ""DE""
            },
            {
                ""name"": ""Ghana"",
                ""short"": ""GH""
            },
            {
                ""name"": ""Greece"",
                ""short"": ""GR""
            },
            {
                ""name"": ""Grenada"",
                ""short"": ""GD""
            },
            {
                ""name"": ""Guatemala"",
                ""short"": ""GT""
            },
            {
                ""name"": ""Guinea"",
                ""short"": ""GN""
            },
            {
                ""name"": ""Guinea-Bissau"",
                ""short"": ""GW""
            },
            {
                ""name"": ""Guyana"",
                ""short"": ""GY""
            },
            {
                ""name"": ""Haiti"",
                ""short"": ""HT""
            },
            {
                ""name"": ""Vatican"",
                ""short"": ""VA""
            },
            {
                ""name"": ""Honduras"",
                ""short"": ""HN""
            },
            {
                ""name"": ""Hungary"",
                ""short"": ""HU""
            },
            {
                ""name"": ""Iceland"",
                ""short"": ""IS""
            },
            {
                ""name"": ""India"",
                ""short"": ""IN""
            },
            {
                ""name"": ""Indonesia"",
                ""short"": ""ID""
            },
            {
                ""name"": ""Iran"",
                ""short"": ""IR""
            },
            {
                ""name"": ""Iraq"",
                ""short"": ""IQ""
            },
            {
                ""name"": ""Ireland"",
                ""short"": ""IE""
            },
            {
                ""name"": ""Israel"",
                ""short"": ""IL""
            },
            {
                ""name"": ""Italy"",
                ""short"": ""IT""
            },
            {
                ""name"": ""Jamaica"",
                ""short"": ""JM""
            },
            {
                ""name"": ""Japan"",
                ""short"": ""JP""
            },
            {
                ""name"": ""Jordan"",
                ""short"": ""JO""
            },
            {
                ""name"": ""Kazakhstan"",
                ""short"": ""KZ""
            },
            {
                ""name"": ""Kenya"",
                ""short"": ""KE""
            },
            {
                ""name"": ""Kiribati"",
                ""short"": ""KI""
            },
            {
                ""name"": ""North Korea"",
                ""short"": ""KP""
            },
            {
                ""name"": ""South Korea"",
                ""short"": ""KR""
            },
            {
                ""name"": ""Kuwait"",
                ""short"": ""KW""
            },
            {
                ""name"": ""Kyrgyzstan"",
                ""short"": ""KG""
            },
            {
                ""name"": ""Latvia"",
                ""short"": ""LV""
            },
            {
                ""name"": ""Lebanon"",
                ""short"": ""LB""
            },
            {
                ""name"": ""Lesotho"",
                ""short"": ""LS""
            },
            {
                ""name"": ""Liberia"",
                ""short"": ""LR""
            },
            {
                ""name"": ""Libya"",
                ""short"": ""LY""
            },
            {
                ""name"": ""Liechtenstein"",
                ""short"": ""LI""
            },
            {
                ""name"": ""Lithuania"",
                ""short"": ""LT""
            },
            {
                ""name"": ""Luxembourg"",
                ""short"": ""LU""
            },
            {
                ""name"": ""Madagascar"",
                ""short"": ""MG""
            },
            {
                ""name"": ""Malawi"",
                ""short"": ""MW""
            },
            {
                ""name"": ""Malaysia"",
                ""short"": ""MY""
            },
            {
                ""name"": ""Maldives"",
                ""short"": ""MV""
            },
            {
                ""name"": ""Mali"",
                ""short"": ""ML""
            },
            {
                ""name"": ""Malta"",
                ""short"": ""MT""
            },
            {
                ""name"": ""Marshall Islands "",
                ""short"": ""MH""
            },
            {
                ""name"": ""Mauritania"",
                ""short"": ""MR""
            },
            {
                ""name"": ""Mauritius"",
                ""short"": ""MU""
            },
            {
                ""name"": ""Mexico"",
                ""short"": ""MX""
            },
            {
                ""name"": ""Micronesia"",
                ""short"": ""FM""
            },
            {
                ""name"": ""Moldova"",
                ""short"": ""MD""
            },
            {
                ""name"": ""Monaco"",
                ""short"": ""MC""
            },
            {
                ""name"": ""Mongolia"",
                ""short"": ""MN""
            },
            {
                ""name"": ""Montenegro"",
                ""short"": ""ME""
            },
            {
                ""name"": ""Morocco"",
                ""short"": ""MA""
            },
            {
                ""name"": ""Mozambique"",
                ""short"": ""MZ""
            },
            {
                ""name"": ""Myanmar "",
                ""short"": ""MM""
            },
            {
                ""name"": ""Namibia"",
                ""short"": ""NA""
            },
            {
                ""name"": ""Nauru"",
                ""short"": ""NR""
            },
            {
                ""name"": ""Nepal"",
                ""short"": ""NP""
            },
            {
                ""name"": ""Netherlands"",
                ""short"": ""NL""
            },
            {
                ""name"": ""New Zealand"",
                ""short"": ""NZ""
            },
            {
                ""name"": ""Nicaragua"",
                ""short"": ""NI""
            },
            {
                ""name"": ""Niger "",
                ""short"": ""NE""
            },
            {
                ""name"": ""Nigeria"",
                ""short"": ""NG""
            },
            {
                ""name"": ""North Macedonia "",
                ""short"": ""MK""
            },
            {
                ""name"": ""Norway"",
                ""short"": ""NO""
            },
            {
                ""name"": ""Oman"",
                ""short"": ""OM""
            },
            {
                ""name"": ""Pakistan"",
                ""short"": ""PK""
            },
            {
                ""name"": ""Palau"",
                ""short"": ""PW""
            },
            {
                ""name"": ""Palestine"",
                ""short"": ""PS""
            },
            {
                ""name"": ""Panama"",
                ""short"": ""PA""
            },
            {
                ""name"": ""Papua New Guinea"",
                ""short"": ""PG""
            },
            {
                ""name"": ""Paraguay"",
                ""short"": ""PY""
            },
            {
                ""name"": ""Peru"",
                ""short"": ""PE""
            },
            {
                ""name"": ""Philippines "",
                ""short"": ""PH""
            },
            {
                ""name"": ""Poland"",
                ""short"": ""PL""
            },
            {
                ""name"": ""Portugal"",
                ""short"": ""PT""
            },
            {
                ""name"": ""Qatar"",
                ""short"": ""QA""
            },
            {
                ""name"": ""Romania"",
                ""short"": ""RO""
            },
            {
                ""name"": ""Russia"",
                ""short"": ""RU""
            },
            {
                ""name"": ""Rwanda"",
                ""short"": ""RW""
            },
            {
                ""name"": ""Saint Kitts and Nevis"",
                ""short"": ""KN""
            },
            {
                ""name"": ""Saint Lucia"",
                ""short"": ""LC""
            },
            {
                ""name"": ""Saint Vincent and the Grenadines"",
                ""short"": ""VC""
            },
            {
                ""name"": ""Samoa"",
                ""short"": ""WS""
            },
            {
                ""name"": ""San Marino"",
                ""short"": ""SM""
            },
            {
                ""name"": ""São Tomé and Príncipe"",
                ""short"": ""ST""
            },
            {
                ""name"": ""Saudi Arabia"",
                ""short"": ""SA""
            },
            {
                ""name"": ""Senegal"",
                ""short"": ""SN""
            },
            {
                ""name"": ""Serbia"",
                ""short"": ""RS""
            },
            {
                ""name"": ""Seychelles"",
                ""short"": ""SC""
            },
            {
                ""name"": ""Sierra Leone"",
                ""short"": ""SL""
            },
            {
                ""name"": ""Singapore"",
                ""short"": ""SG""
            },
            {
                ""name"": ""Slovakia"",
                ""short"": ""SK""
            },
            {
                ""name"": ""Slovenia"",
                ""short"": ""SI""
            },
            {
                ""name"": ""Solomon Islands"",
                ""short"": ""SB""
            },
            {
                ""name"": ""Somalia"",
                ""short"": ""SO""
            },
            {
                ""name"": ""South Africa"",
                ""short"": ""ZA""
            },
            {
                ""name"": ""South Sudan"",
                ""short"": ""SS""
            },
            {
                ""name"": ""Spain"",
                ""short"": ""ES""
            },
            {
                ""name"": ""Sri Lanka"",
                ""short"": ""LK""
            },
            {
                ""name"": ""Sudan "",
                ""short"": ""SD""
            },
            {
                ""name"": ""Suriname"",
                ""short"": ""SR""
            },
            {
                ""name"": ""Sweden"",
                ""short"": ""SE""
            },
            {
                ""name"": ""Switzerland"",
                ""short"": ""CH""
            },
            {
                ""name"": ""Syria "",
                ""short"": ""SY""
            },
            {
                ""name"": ""Tajikistan"",
                ""short"": ""TJ""
            },
            {
                ""name"": ""Tanzania"",
                ""short"": ""TZ""
            },
            {
                ""name"": ""Thailand"",
                ""short"": ""TH""
            },
            {
                ""name"": ""Timor-Leste"",
                ""short"": ""TL""
            },
            {
                ""name"": ""Togo"",
                ""short"": ""TG""
            },
            {
                ""name"": ""Tonga"",
                ""short"": ""TO""
            },
            {
                ""name"": ""Trinidad and Tobago"",
                ""short"": ""TT""
            },
            {
                ""name"": ""Tunisia"",
                ""short"": ""TN""
            },
            {
                ""name"": ""Turkey "",
                ""short"": ""TR""
            },
            {
                ""name"": ""Turkmenistan"",
                ""short"": ""TM""
            },
            {
                ""name"": ""Tuvalu"",
                ""short"": ""TV""
            },
            {
                ""name"": ""Uganda"",
                ""short"": ""UG""
            },
            {
                ""name"": ""Ukraine"",
                ""short"": ""UA""
            },
            {
                ""name"": ""United Arab Emirates (UAE)"",
                ""short"": ""AE""
            },
            {
                ""name"": ""United Kingdom"",
                ""short"": ""GB""
            },
            {
                ""name"": ""USA"",
                ""short"": ""US""
            },
            {
                ""name"": ""Uruguay"",
                ""short"": ""UY""
            },
            {
                ""name"": ""Uzbekistan"",
                ""short"": ""UZ""
            },
            {
                ""name"": ""Vanuatu"",
                ""short"": ""VU""
            },
            {
                ""name"": ""Venezuela"",
                ""short"": ""VE""
            },
            {
                ""name"": ""Vietnam"",
                ""short"": ""VN""
            },
            {
                ""name"": ""Yemen"",
                ""short"": ""YE""
            },
            {
                ""name"": ""Zambia"",
                ""short"": ""ZM""
            },
            {
                ""name"": ""Zimbabwe"",
                ""short"": ""ZW""
            }
        ]
    }
        ";

        #endregion

        var dataContainer = JsonSerializer.Deserialize<CountriesContainer>( json );

        foreach (var c in dataContainer.countries ) { 
            c.name = c.name.Trim();
            var match = pop.Where(  p=>p.country.ToLower() == c.name.ToLower() ).FirstOrDefault();
            if ( match==null ) {
                //throw new Exception(  "no pop data " + c.name.ToLower() );
            } else {
                c.population = match.population;
            }
        }
        return dataContainer.countries;
    }

    public class  CountriesContainer { 
        public List<Country> countries {get; set; }
    }

    public class Country {
        public string name {get; set; }
        public string @short {get; set; }
        public long population { get; set; }
    }

    public static List<PopData> pop = new List<PopData>()
    {
        new PopData()
        {
            country = "China",
            population = 1409670000
        },
        new PopData()
        {
            country = "India",
            population = 1400744000,
        },
        new PopData()
        {
            country = "United States",
            population = 335893238
        },
        new PopData()
        {
            country = "Indonesia",
            population = 279118866
        },
        new PopData()
        {
            country = "Pakistan",
            population = 241499431
        },
        new PopData()
        {
            country = "Nigeria",
            population = 223800000
        },
        new PopData()
        {
            country = "Brazil",
            population = 203080756
        },
        new PopData()
        {
            country = "Bangladesh",
            population = 169828911
        },
        new PopData()
        {
            country = "Russia",
            population = 146150789
        },
        new PopData()
        {
            country = "Mexico",
            population = 129713690
        },
        new PopData()
        {
            country = "Japan",
            population = 123930000
        },
        new PopData()
        {
            country = "Philippines",
            population = 112892781
        },
        new PopData()
        {
            country = "Ethiopia",
            population = 107334000
        },
        new PopData()
        {
            country = "Egypt",
            population = 105914499
        },
        new PopData()
        {
            country = "Vietnam",
            population = 100300000
        },
        new PopData()
        {
            country = "Democratic Republic of the Congo (DRC)",
            population = 95370000
        },
        new PopData()
        {
            country = "Turkey",
            population = 85372377
        },
        new PopData()
        {
            country = "Germany",
            population = 84607016
        },
        new PopData()
        {
            country = "Iran",
            population = 84055000
        },
        new PopData()
        {
            country = "France",
            population = 68428000
        },
        new PopData()
        {
            country = "United Kingdom",
            population = 67596281
        },
        new PopData()
        {
            country = "Thailand",
            population = 65990480
        },
        new PopData()
        {
            country = "South Africa",
            population = 62027503
        },
        new PopData()
        {
            country = "Tanzania",
            population = 61741120
        },
        new PopData()
        {
            country = "Italy",
            population = 58971638
        },
        new PopData()
        {
            country = "Myanmar",
            population = 55770232
        },
        new PopData()
        {
            country = "Colombia",
            population = 52695952
        },
        new PopData()
        {
            country = "Kenya",
            population = 51526000
        },
        new PopData()
        {
            country = "South Korea",
            population = 51285153
        },
        new PopData()
        {
            country = "Spain",
            population = 48692804
        },
        new PopData()
        {
            country = "Argentina",
            population = 46654581
        },
        new PopData()
        {
            country = "Uganda",
            population = 45562000
        },
        new PopData()
        {
            country = "Algeria",
            population = 45400000
        },
        new PopData()
        {
            country = "Iraq",
            population = 43324000
        },
        new PopData()
        {
            country = "Sudan",
            population = 41984500
        },
        new PopData()
        {
            country = "Canada",
            population = 40769890
        },
        new PopData()
        {
            country = "Poland",
            population = 37595000
        },
        new PopData()
        {
            country = "Morocco",
            population = 37022000
        },
        new PopData()
        {
            country = "Uzbekistan",
            population = 36963262
        },
        new PopData()
        {
            country = "Ukraine",
            population = 36700000
        },
        new PopData()
        {
            country = "Afghanistan",
            population = 34262840
        },
        new PopData()
        {
            country = "Angola",
            population = 34094077
        },
        new PopData()
        {
            country = "Peru",
            population = 33725844
        },
        new PopData()
        {
            country = "Malaysia",
            population = 33379500
        },
        new PopData()
        {
            country = "Mozambique",
            population = 32419747
        },
        new PopData()
        {
            country = "Saudi Arabia",
            population = 32175224
        },
        new PopData()
        {
            country = "Yemen",
            population = 31888698
        },
        new PopData()
        {
            country = "Ghana",
            population = 30832019
        },
        new PopData()
        {
            country = "Côte d'Ivoire",
            population = 29389150
        },
        new PopData()
        {
            country = "Nepal",
            population = 29164578
        },
        new PopData()
        {
            country = "Venezuela",
            population = 28302000
        },
        new PopData()
        {
            country = "Cameroon",
            population = 28088845
        },
        new PopData()
        {
            country = "Madagascar",
            population = 26923353
        },
        new PopData()
        {
            country = "Australia",
            population = 26821557
        },
        new PopData()
        {
            country = "North Korea",
            population = 25660000
        },
        new PopData()
        {
            country = "Niger",
            population = 25369415
        },
        new PopData()
        {
            country = "Taiwan",
            population = 23420442
        },
        new PopData()
        {
            country = "Syria",
            population = 22923000
        },
        new PopData()
        {
            country = "Burkina Faso",
            population = 22752315
        },
        new PopData()
        {
            country = "Mali",
            population = 22395489
        },
        new PopData()
        {
            country = "Sri Lanka",
            population = 22037000
        },
        new PopData()
        {
            country = "Malawi",
            population = 21507723
        },
        new PopData()
        {
            country = "Kazakhstan",
            population = 20095963
        },
        new PopData()
        {
            country = "Chile",
            population = 19960889
        },
        new PopData()
        {
            country = "Zambia",
            population = 19610769
        },
        new PopData()
        {
            country = "Romania",
            population = 19051562
        },
        new PopData()
        {
            country = "Senegal",
            population = 18275743
        },
        new PopData()
        {
            country = "Somalia",
            population = 18143_379
        },
        new PopData()
        {
            country = "Netherlands",
            population = 17967505
        },
        new PopData()
        {
            country = "Guatemala",
            population = 17602431
        },
        new PopData()
        {
            country = "Chad",
            population = 17414717
        },
        new PopData()
        {
            country = "Cambodia",
            population = 17091464
        },
        new PopData()
        {
            country = "Ecuador",
            population = 16938986
        },
        new PopData()
        {
            country = "Zimbabwe",
            population = 15178979
        },
        new PopData()
        {
            country = "South Sudan",
            population = 14746494
        },
        new PopData()
        {
            country = "Guinea",
            population = 13261638
        },
        new PopData()
        {
            country = "Rwanda",
            population = 13246394
        },
        new PopData()
        {
            country = "Burundi",
            population = 12837740
        },
        new PopData()
        {
            country = "Benin",
            population = 12606998
        },
        new PopData()
        {
            country = "Bolivia",
            population = 12006031
        },
        new PopData()
        {
            country = "Tunisia",
            population = 11850232
        },
        new PopData()
        {
            country = "Belgium",
            population = 11820117
        },
        new PopData()
        {
            country = "Papua New Guinea",
            population = 11781559
        },
        new PopData()
        {
            country = "Haiti",
            population = 11743017
        },
        new PopData()
        {
            country = "Jordan",
            population = 11516000
        },
        new PopData()
        {
            country = "Cuba",
            population = 11089511
        },
        new PopData()
        {
            country = "Czech Republic",
            population = 10900555
        },
        new PopData()
        {
            country = "Dominican Republic",
            population = 10760028
        },
        new PopData()
        {
            country = "Sweden",
            population = 10548822
        },
        new PopData()
        {
            country = "Portugal",
            population = 10467366
        },
        new PopData()
        {
            country = "Greece",
            population = 10413982
        },
        new PopData()
        {
            country = "Azerbaijan",
            population = 10151517
        },
        new PopData()
        {
            country = "Tajikistan",
            population = 10077600
        },
        new PopData()
        {
            country = "Israel",
            population = 9892000
        },
        new PopData()
        {
            country = "Honduras",
            population = 9745149
        },
        new PopData()
        {
            country = "Hungary",
            population = 9584000
        },
        new PopData()
        {
            country = "United Arab Emirates",
            population = 9282410
        },
        new PopData()
        {
            country = "Austria",
            population = 9170647
        },
        new PopData()
        {
            country = "Belarus",
            population = 9155978
        },
        new PopData()
        {
            country = "Switzerland",
            population = 8960817
        },
        new PopData()
        {
            country = "Sierra Leone",
            population = 8494260
        },
        new PopData()
        {
            country = "Togo",
            population = 8095498
        },
        new PopData()
        {
            country = "Hong Kong (China)",
            population = 7498100
        },
        new PopData()
        {
            country = "Laos",
            population = 7443000
        },
        new PopData()
        {
            country = "Kyrgyzstan",
            population = 7100000
        },
        new PopData()
        {
            country = "Turkmenistan",
            population = 7057841
        },
        new PopData()
        {
            country = "Libya",
            population = 6931061
        },
        new PopData()
        {
            country = "El Salvador",
            population = 6884888
        },
        new PopData()
        {
            country = "Nicaragua",
            population = 6733763
        },
        new PopData()
        {
            country = "Serbia",
            population = 6641197
        },
        new PopData()
        {
            country = "Bulgaria",
            population = 6445481
        },
        new PopData()
        {
            country = "Republic of the Congo",
            population = 6142180
        },
        new PopData()
        {
            country = "Paraguay",
            population = 6109644
        },
        new PopData()
        {
            country = "Denmark",
            population = 5967824
        },
        new PopData()
        {
            country = "Singapore",
            population = 5917600
        },
        new PopData()
        {
            country = "Central African Republic",
            population = 5633412
        },
        new PopData()
        {
            country = "Finland",
            population = 5583385
        },
        new PopData()
        {
            country = "Norway",
            population = 5562363
        },
        new PopData()
        {
            country = "Lebanon",
            population = 5490000
        },
        new PopData()
        {
            country = "Palestine",
            population = 5483450
        },
        new PopData()
        {
            country = "Slovakia",
            population = 5424687
        },
        new PopData()
        {
            country = "Ireland",
            population = 5281600
        },
        new PopData()
        {
            country = "New Zealand",
            population = 5305600
        },
        new PopData()
        {
            country = "Costa Rica",
            population = 5262225
        },
        new PopData()
        {
            country = "Liberia",
            population = 5248621
        },
        new PopData()
        {
            country = "Oman",
            population = 5113071
        },
        new PopData()
        {
            country = "Kuwait",
            population = 4670713
        },
        new PopData()
        {
            country = "Mauritania",
            population = 4475683
        },
        new PopData()
        {
            country = "Panama",
            population = 4064780
        },
        new PopData()
        {
            country = "Croatia",
            population = 3855641
        },
        new PopData()
        {
            country = "Eritrea",
            population = 3748902
        },
        new PopData()
        {
            country = "Georgia",
            population = 3694600
        },
        new PopData()
        {
            country = "Mongolia",
            population = 3457548
        },
        new PopData()
        {
            country = "Uruguay",
            population = 3444263
        },
        new PopData()
        {
            country = "Bosnia and Herzegovina",
            population = 3277082
        },
        new PopData()
        {
            country = "Puerto Rico (US)",
            population = 3205691
        },
        new PopData()
        {
            country = "Namibia",
            population = 3022401
        },
        new PopData()
        {
            country = "Armenia",
            population = 2993800
        },
        new PopData()
        {
            country = "Lithuania",
            population = 2886515
        },
        new PopData()
        {
            country = "Jamaica",
            population = 2825544
        },
        new PopData()
        {
            country = "Albania",
            population = 2761785
        },
        new PopData()
        {
            country = "Qatar",
            population = 2656032
        },
        new PopData()
        {
            country = "Moldova",
            population = 2512758
        },
        new PopData()
        {
            country = "Gambia",
            population = 2417471
        },
        new PopData()
        {
            country = "Botswana",
            population = 2410338
        },
        new PopData()
        {
            country = "Lesotho",
            population = 2306000
        },
        new PopData()
        {
            country = "Gabon",
            population = 2233272
        },
        new PopData()
        {
            country = "Slovenia",
            population = 2123103
        },
        new PopData()
        {
            country = "Latvia",
            population = 1872500
        },
        new PopData()
        {
            country = "North Macedonia",
            population = 1832696
        },
        new PopData()
        {
            country = "Guinea-Bissau",
            population = 1781308
        },
        new PopData()
        {
            country = "Kosovo",
            population = 1762220
        },
        new PopData()
        {
            country = "Bahrain",
            population = 1577059
        },
        new PopData()
        {
            country = "Equatorial Guinea",
            population = 1558160
        },
        new PopData()
        {
            country = "Trinidad and Tobago",
            population = 1367510
        },
        new PopData()
        {
            country = "Estonia",
            population = 1366491
        },
        new PopData()
        {
            country = "East Timor",
            population = 1354662
        },
        new PopData()
        {
            country = "Mauritius",
            population = 1261041
        },
        new PopData()
        {
            country = "Eswatini",
            population = 1223362
        },
        new PopData()
        {
            country = "Djibouti",
            population = 1001454
        },
        new PopData()
        {
            country = "Cyprus",
            population = 918100
        },
        new PopData()
        {
            country = "Fiji",
            population = 893468
        },
        new PopData()
        {
            country = "Bhutan",
            population = 770276
        },
        new PopData()
        {
            country = "Comoros",
            population = 758316
        },
        new PopData()
        {
            country = "Guyana",
            population = 743699
        },
        new PopData()
        {
            country = "Solomon Islands",
            population = 734887
        },
        new PopData()
        {
            country = "Macau (China)",
            population = 683700
        },
        new PopData()
        {
            country = "Luxembourg",
            population = 672020
        },
        new PopData()
        {
            country = "Montenegro",
            population = 616695
        },
        new PopData()
        {
            country = "Suriname",
            population = 616500
        },
        new PopData()
        {
            country = "Western Sahara",
            population = 587259
        },
        new PopData()
        {
            country = "Malta",
            population = 519562
        },
        new PopData()
        {
            country = "Maldives",
            population = 515132
        },
        new PopData()
        {
            country = "Cabo Verde",
            population = 491233
        },
        new PopData()
        {
            country = "Brunei",
            population = 445400
        },
        new PopData()
        {
            country = "Belize",
            population = 397483
        },
        new PopData()
        {
            country = "Bahamas",
            population = 397360
        },
        new PopData()
        {
            country = "Iceland",
            population = 383726
        },
        new PopData()
        {
            country = "Northern Cyprus",
            population = 382836
        },
        new PopData()
        {
            country = "Transnistria",
            population = 360938
        },
        new PopData()
        {
            country = "Vanuatu",
            population = 301295
        },
        new PopData()
        {
            country = "French Polynesia (France)",
            population = 279890
        },
        new PopData()
        {
            country = "New Caledonia (France)",
            population = 268510
        },
        new PopData()
        {
            country = "Barbados",
            population = 267800
        },
        new PopData()
        {
            country = "Abkhazia",
            population = 244236
        },
        new PopData()
        {
            country = "São Tomé and Príncipe",
            population = 214610
        },
        new PopData()
        {
            country = "Samoa",
            population = 205557
        },
        new PopData()
        {
            country = "Saint Lucia",
            population = 178696
        },
        new PopData()
        {
            country = "Guam (US)",
            population = 153836
        },
        new PopData()
        {
            country = "Curacao (Netherlands)",
            population = 148925
        },
        new PopData()
        {
            country = "Kiribati",
            population = 120740
        },
        new PopData()
        {
            country = "Grenada",
            population = 112579
        },
        new PopData()
        {
            country = "Saint Vincent and the Grenadines",
            population = 110872
        },
        new PopData()
        {
            country = "Aruba (Netherlands)",
            population = 106739
        },
        new PopData()
        {
            country = "Micronesia",
            population = 105754
        },
        new PopData()
        {
            country = "Jersey (UK)",
            population = 103267
        },
        new PopData()
        {
            country = "Antigua and Barbuda",
            population = 100772
        },
        new PopData()
        {
            country = "Seychelles",
            population = 100447
        },
        new PopData()
        {
            country = "Tonga",
            population = 100179
        },
        new PopData()
        {
            country = "U.S. Virgin Islands (US)",
            population = 87146
        },
        new PopData()
        {
            country = "Andorra",
            population = 85101
        },
        new PopData()
        {
            country = "Isle of Man (UK)",
            population = 84530
        },
        new PopData()
        {
            country = "Cayman Islands (UK)",
            population = 71105
        },
        new PopData()
        {
            country = "Dominica",
            population = 67408
        },
        new PopData()
        {
            country = "Guernsey (UK)",
            population = 64091
        },
        new PopData()
        {
            country = "Bermuda (UK)",
            population = 64055
        },
        new PopData()
        {
            country = "Greenland (Denmark)",
            population = 56865
        },
        new PopData()
        {
            country = "South Ossetia",
            population = 56520
        },
        new PopData()
        {
            country = "Faroe Islands (Denmark)",
            population = 54547
        },
        new PopData()
        {
            country = "American Samoa (US)",
            population = 49710
        },
        new PopData()
        {
            country = "Northern Mariana Islands (US)",
            population = 47329
        },
        new PopData()
        {
            country = "Saint Kitts and Nevis",
            population = 47195
        },
        new PopData()
        {
            country = "Turks and Caicos Islands (UK)",
            population = 46131
        },
        new PopData()
        {
            country = "Sint Maarten (Netherlands)",
            population = 42938
        },
        new PopData()
        {
            country = "Marshall Islands",
            population = 42418
        },
        new PopData()
        {
            country = "Liechtenstein",
            population = 40023
        },
        new PopData()
        {
            country = "Monaco",
            population = 38367
        },
        new PopData()
        {
            country = "Gibraltar (UK)",
            population = 34003
        },
        new PopData()
        {
            country = "San Marino",
            population = 33916
        },
        new PopData()
        {
            country = "Saint Martin (France)",
            population = 32358
        },
        new PopData()
        {
            country = "British Virgin Islands (UK)",
            population = 31538
        },
        new PopData()
        {
            country = "Åland (Finland)",
            population = 30587
        },
        new PopData()
        {
            country = "Palau",
            population = 16733
        },
        new PopData()
        {
            country = "Anguilla (UK)",
            population = 15701
        },
        new PopData()
        {
            country = "Cook Islands",
            population = 15040
        },
        new PopData()
        {
            country = "Nauru",
            population = 11680
        },
        new PopData()
        {
            country = "Wallis and Futuna (France)",
            population = 11369
        },
        new PopData()
        {
            country = "Tuvalu",
            population = 10679
        },
        new PopData()
        {
            country = "Saint Barthélemy (France)",
            population = 10585
        },
        new PopData()
        {
            country = "Saint Pierre and Miquelon (France)",
            population = 6092
        },
        new PopData()
        {
            country = "Saint Helena, Ascension and Tristan da Cunha (UK)",
            population = 5651
        },
        new PopData()
        {
            country = "Montserrat (UK)",
            population = 4433
        },
        new PopData()
        {
            country = "Falkland Islands (UK)",
            population = 3662
        },
        new PopData()
        {
            country = "Norfolk Island (Australia)",
            population = 2188
        },
        new PopData()
        {
            country = "Christmas Island (Australia)",
            population = 1692
        },
        new PopData()
        {
            country = "Niue",
            population = 1689
        },
        new PopData()
        {
            country = "Tokelau (NZ)",
            population = 1647
        },
        new PopData()
        {
            country = "Vatican City",
            population = 764
        },
        new PopData()
        {
            country = "Cocos (Keeling) Islands (Australia)",
            population = 593
        },
        new PopData()
        {
            country = "Pitcairn Islands (UK)",
            population = 47
        },
        new PopData()
        {
            country = "Pitcairn Islands (UK)",
            population = 47
        },

    };
}

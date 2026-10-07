using FormulariosUAF.Models.Domain;

namespace FormulariosUAF.Data;

/// <summary>
/// Filas iniciales de los catálogos T_TIPODOCUMENTO, T_PAIS y T_NACIONALIDAD (HasData).
/// Los Id son fijos: para agregar un país/nacionalidad, usar el siguiente Id libre al final
/// (nunca renumerar) y crear una migración. T_PAIS y T_NACIONALIDAD comparten Id por país.
/// </summary>
public static class CatalogosSeed
{
    public static readonly TipoDocumento[] TiposDocumento =
    [
        new() { Id = TipoDocumento.IdRut, Nombre = "RUT", Orden = 1 },
        new() { Id = 2, Nombre = "Pasaporte", Orden = 2 },
        new() { Id = 3, Nombre = "Cédula de identidad extranjera", Orden = 3 },
        new() { Id = 4, Nombre = "Otro", Orden = 4 },
    ];

    // Id|País|Nacionalidad
    private const string Datos = """
        1|Afganistán|Afgana
        2|Albania|Albanesa
        3|Alemania|Alemana
        4|Andorra|Andorrana
        5|Angola|Angoleña
        6|Antigua y Barbuda|Antiguana
        7|Arabia Saudita|Saudí
        8|Argelia|Argelina
        9|Argentina|Argentina
        10|Armenia|Armenia
        11|Australia|Australiana
        12|Austria|Austriaca
        13|Azerbaiyán|Azerbaiyana
        14|Bahamas|Bahameña
        15|Bangladés|Bangladesí
        16|Barbados|Barbadense
        17|Baréin|Bareiní
        18|Bélgica|Belga
        19|Belice|Beliceña
        20|Benín|Beninesa
        21|Bielorrusia|Bielorrusa
        22|Bolivia|Boliviana
        23|Bosnia y Herzegovina|Bosnia
        24|Botsuana|Botsuana
        25|Brasil|Brasileña
        26|Brunéi|Bruneana
        27|Bulgaria|Búlgara
        28|Burkina Faso|Burkinesa
        29|Burundi|Burundesa
        30|Bután|Butanesa
        31|Cabo Verde|Caboverdiana
        32|Camboya|Camboyana
        33|Camerún|Camerunesa
        34|Canadá|Canadiense
        35|Catar|Catarí
        36|Chad|Chadiana
        37|Chile|Chilena
        38|China|China
        39|Chipre|Chipriota
        40|Colombia|Colombiana
        41|Comoras|Comorense
        42|Congo|Congoleña
        43|Corea del Norte|Norcoreana
        44|Corea del Sur|Surcoreana
        45|Costa de Marfil|Marfileña
        46|Costa Rica|Costarricense
        47|Croacia|Croata
        48|Cuba|Cubana
        49|Dinamarca|Danesa
        50|Dominica|Dominiquesa
        51|Ecuador|Ecuatoriana
        52|Egipto|Egipcia
        53|El Salvador|Salvadoreña
        54|Emiratos Árabes Unidos|Emiratí
        55|Eritrea|Eritrea
        56|Eslovaquia|Eslovaca
        57|Eslovenia|Eslovena
        58|España|Española
        59|Estados Unidos|Estadounidense
        60|Estonia|Estonia
        61|Esuatini|Suazi
        62|Etiopía|Etíope
        63|Filipinas|Filipina
        64|Finlandia|Finlandesa
        65|Fiyi|Fiyiana
        66|Francia|Francesa
        67|Gabón|Gabonesa
        68|Gambia|Gambiana
        69|Georgia|Georgiana
        70|Ghana|Ghanesa
        71|Granada|Granadina
        72|Grecia|Griega
        73|Guatemala|Guatemalteca
        74|Guinea|Guineana
        75|Guinea Ecuatorial|Ecuatoguineana
        76|Guinea-Bisáu|Bisauguineana
        77|Guyana|Guyanesa
        78|Haití|Haitiana
        79|Honduras|Hondureña
        80|Hungría|Húngara
        81|India|India
        82|Indonesia|Indonesia
        83|Irak|Iraquí
        84|Irán|Iraní
        85|Irlanda|Irlandesa
        86|Islandia|Islandesa
        87|Islas Marshall|Marshalesa
        88|Islas Salomón|Salomonense
        89|Israel|Israelí
        90|Italia|Italiana
        91|Jamaica|Jamaicana
        92|Japón|Japonesa
        93|Jordania|Jordana
        94|Kazajistán|Kazaja
        95|Kenia|Keniana
        96|Kirguistán|Kirguisa
        97|Kiribati|Kiribatiana
        98|Kuwait|Kuwaití
        99|Laos|Laosiana
        100|Lesoto|Lesotense
        101|Letonia|Letona
        102|Líbano|Libanesa
        103|Liberia|Liberiana
        104|Libia|Libia
        105|Liechtenstein|Liechtensteiniana
        106|Lituania|Lituana
        107|Luxemburgo|Luxemburguesa
        108|Macedonia del Norte|Macedonia
        109|Madagascar|Malgache
        110|Malasia|Malasia
        111|Malaui|Malauí
        112|Maldivas|Maldiva
        113|Malí|Maliense
        114|Malta|Maltesa
        115|Marruecos|Marroquí
        116|Mauricio|Mauriciana
        117|Mauritania|Mauritana
        118|México|Mexicana
        119|Micronesia|Micronesia
        120|Moldavia|Moldava
        121|Mónaco|Monegasca
        122|Mongolia|Mongola
        123|Montenegro|Montenegrina
        124|Mozambique|Mozambiqueña
        125|Myanmar|Birmana
        126|Namibia|Namibia
        127|Nauru|Nauruana
        128|Nepal|Nepalí
        129|Nicaragua|Nicaragüense
        130|Níger|Nigerina
        131|Nigeria|Nigeriana
        132|Noruega|Noruega
        133|Nueva Zelanda|Neozelandesa
        134|Omán|Omaní
        135|Países Bajos|Neerlandesa
        136|Pakistán|Pakistaní
        137|Palaos|Palauana
        138|Palestina|Palestina
        139|Panamá|Panameña
        140|Papúa Nueva Guinea|Papú
        141|Paraguay|Paraguaya
        142|Perú|Peruana
        143|Polonia|Polaca
        144|Portugal|Portuguesa
        145|Reino Unido|Británica
        146|República Centroafricana|Centroafricana
        147|República Checa|Checa
        148|República Democrática del Congo|Congoleña (RDC)
        149|República Dominicana|Dominicana
        150|Ruanda|Ruandesa
        151|Rumania|Rumana
        152|Rusia|Rusa
        153|Samoa|Samoana
        154|San Cristóbal y Nieves|Sancristobaleña
        155|San Marino|Sanmarinense
        156|San Vicente y las Granadinas|Sanvicentina
        157|Santa Lucía|Santaluciense
        158|Santo Tomé y Príncipe|Santotomense
        159|Senegal|Senegalesa
        160|Serbia|Serbia
        161|Seychelles|Seychellense
        162|Sierra Leona|Sierraleonesa
        163|Singapur|Singapurense
        164|Siria|Siria
        165|Somalia|Somalí
        166|Sri Lanka|Esrilanquesa
        167|Sudáfrica|Sudafricana
        168|Sudán|Sudanesa
        169|Sudán del Sur|Sursudanesa
        170|Suecia|Sueca
        171|Suiza|Suiza
        172|Surinam|Surinamesa
        173|Tailandia|Tailandesa
        174|Taiwán|Taiwanesa
        175|Tanzania|Tanzana
        176|Tayikistán|Tayika
        177|Timor Oriental|Timorense
        178|Togo|Togolesa
        179|Tonga|Tongana
        180|Trinidad y Tobago|Trinitense
        181|Túnez|Tunecina
        182|Turkmenistán|Turcomana
        183|Turquía|Turca
        184|Tuvalu|Tuvaluana
        185|Ucrania|Ucraniana
        186|Uganda|Ugandesa
        187|Uruguay|Uruguaya
        188|Uzbekistán|Uzbeka
        189|Vanuatu|Vanuatuense
        190|Vaticano|Vaticana
        191|Venezuela|Venezolana
        192|Vietnam|Vietnamita
        193|Yemen|Yemení
        194|Yibuti|Yibutiana
        195|Zambia|Zambiana
        196|Zimbabue|Zimbabuense
        """;

    private static readonly (int Id, string Pais, string Nacionalidad)[] Filas = Datos
        .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(l => l.Split('|'))
        .Select(c => (int.Parse(c[0]), c[1], c[2]))
        .ToArray();

    public static readonly Pais[] Paises = Filas
        .Select(f => new Pais { Id = f.Id, Nombre = f.Pais, Orden = f.Id == Pais.IdChile ? 0 : 10 })
        .ToArray();

    public static readonly Nacionalidad[] Nacionalidades = Filas
        .Select(f => new Nacionalidad { Id = f.Id, Nombre = f.Nacionalidad, Orden = f.Id == Nacionalidad.IdChilena ? 0 : 10 })
        .ToArray();
}
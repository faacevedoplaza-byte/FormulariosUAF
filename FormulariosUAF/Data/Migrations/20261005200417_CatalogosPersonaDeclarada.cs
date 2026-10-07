using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class CatalogosPersonaDeclarada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ST_PAIS_PERSONADECLARADA",
                table: "T_PERSONADECLARADA",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IN_COD_NACIONALIDAD",
                table: "T_PERSONADECLARADA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IN_COD_PAIS",
                table: "T_PERSONADECLARADA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IN_COD_TIPODOCUMENTO",
                table: "T_PERSONADECLARADA",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "T_NACIONALIDAD",
                columns: table => new
                {
                    IN_COD_NACIONALIDAD = table.Column<int>(type: "int", nullable: false),
                    ST_NOMBRE_NACIONALIDAD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IN_ORDEN_NACIONALIDAD = table.Column<int>(type: "int", nullable: false),
                    BO_ACTIVO_NACIONALIDAD = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_NACIONALIDAD", x => x.IN_COD_NACIONALIDAD);
                });

            migrationBuilder.CreateTable(
                name: "T_PAIS",
                columns: table => new
                {
                    IN_COD_PAIS = table.Column<int>(type: "int", nullable: false),
                    ST_NOMBRE_PAIS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IN_ORDEN_PAIS = table.Column<int>(type: "int", nullable: false),
                    BO_ACTIVO_PAIS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PAIS", x => x.IN_COD_PAIS);
                });

            migrationBuilder.CreateTable(
                name: "T_TIPODOCUMENTO",
                columns: table => new
                {
                    IN_COD_TIPODOCUMENTO = table.Column<int>(type: "int", nullable: false),
                    ST_NOMBRE_TIPODOCUMENTO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IN_ORDEN_TIPODOCUMENTO = table.Column<int>(type: "int", nullable: false),
                    BO_ACTIVO_TIPODOCUMENTO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TIPODOCUMENTO", x => x.IN_COD_TIPODOCUMENTO);
                });

            migrationBuilder.InsertData(
                table: "T_NACIONALIDAD",
                columns: new[] { "IN_COD_NACIONALIDAD", "BO_ACTIVO_NACIONALIDAD", "ST_NOMBRE_NACIONALIDAD", "IN_ORDEN_NACIONALIDAD" },
                values: new object[,]
                {
                    { 1, true, "Afgana", 10 },
                    { 2, true, "Albanesa", 10 },
                    { 3, true, "Alemana", 10 },
                    { 4, true, "Andorrana", 10 },
                    { 5, true, "Angoleña", 10 },
                    { 6, true, "Antiguana", 10 },
                    { 7, true, "Saudí", 10 },
                    { 8, true, "Argelina", 10 },
                    { 9, true, "Argentina", 10 },
                    { 10, true, "Armenia", 10 },
                    { 11, true, "Australiana", 10 },
                    { 12, true, "Austriaca", 10 },
                    { 13, true, "Azerbaiyana", 10 },
                    { 14, true, "Bahameña", 10 },
                    { 15, true, "Bangladesí", 10 },
                    { 16, true, "Barbadense", 10 },
                    { 17, true, "Bareiní", 10 },
                    { 18, true, "Belga", 10 },
                    { 19, true, "Beliceña", 10 },
                    { 20, true, "Beninesa", 10 },
                    { 21, true, "Bielorrusa", 10 },
                    { 22, true, "Boliviana", 10 },
                    { 23, true, "Bosnia", 10 },
                    { 24, true, "Botsuana", 10 },
                    { 25, true, "Brasileña", 10 },
                    { 26, true, "Bruneana", 10 },
                    { 27, true, "Búlgara", 10 },
                    { 28, true, "Burkinesa", 10 },
                    { 29, true, "Burundesa", 10 },
                    { 30, true, "Butanesa", 10 },
                    { 31, true, "Caboverdiana", 10 },
                    { 32, true, "Camboyana", 10 },
                    { 33, true, "Camerunesa", 10 },
                    { 34, true, "Canadiense", 10 },
                    { 35, true, "Catarí", 10 },
                    { 36, true, "Chadiana", 10 },
                    { 37, true, "Chilena", 0 },
                    { 38, true, "China", 10 },
                    { 39, true, "Chipriota", 10 },
                    { 40, true, "Colombiana", 10 },
                    { 41, true, "Comorense", 10 },
                    { 42, true, "Congoleña", 10 },
                    { 43, true, "Norcoreana", 10 },
                    { 44, true, "Surcoreana", 10 },
                    { 45, true, "Marfileña", 10 },
                    { 46, true, "Costarricense", 10 },
                    { 47, true, "Croata", 10 },
                    { 48, true, "Cubana", 10 },
                    { 49, true, "Danesa", 10 },
                    { 50, true, "Dominiquesa", 10 },
                    { 51, true, "Ecuatoriana", 10 },
                    { 52, true, "Egipcia", 10 },
                    { 53, true, "Salvadoreña", 10 },
                    { 54, true, "Emiratí", 10 },
                    { 55, true, "Eritrea", 10 },
                    { 56, true, "Eslovaca", 10 },
                    { 57, true, "Eslovena", 10 },
                    { 58, true, "Española", 10 },
                    { 59, true, "Estadounidense", 10 },
                    { 60, true, "Estonia", 10 },
                    { 61, true, "Suazi", 10 },
                    { 62, true, "Etíope", 10 },
                    { 63, true, "Filipina", 10 },
                    { 64, true, "Finlandesa", 10 },
                    { 65, true, "Fiyiana", 10 },
                    { 66, true, "Francesa", 10 },
                    { 67, true, "Gabonesa", 10 },
                    { 68, true, "Gambiana", 10 },
                    { 69, true, "Georgiana", 10 },
                    { 70, true, "Ghanesa", 10 },
                    { 71, true, "Granadina", 10 },
                    { 72, true, "Griega", 10 },
                    { 73, true, "Guatemalteca", 10 },
                    { 74, true, "Guineana", 10 },
                    { 75, true, "Ecuatoguineana", 10 },
                    { 76, true, "Bisauguineana", 10 },
                    { 77, true, "Guyanesa", 10 },
                    { 78, true, "Haitiana", 10 },
                    { 79, true, "Hondureña", 10 },
                    { 80, true, "Húngara", 10 },
                    { 81, true, "India", 10 },
                    { 82, true, "Indonesia", 10 },
                    { 83, true, "Iraquí", 10 },
                    { 84, true, "Iraní", 10 },
                    { 85, true, "Irlandesa", 10 },
                    { 86, true, "Islandesa", 10 },
                    { 87, true, "Marshalesa", 10 },
                    { 88, true, "Salomonense", 10 },
                    { 89, true, "Israelí", 10 },
                    { 90, true, "Italiana", 10 },
                    { 91, true, "Jamaicana", 10 },
                    { 92, true, "Japonesa", 10 },
                    { 93, true, "Jordana", 10 },
                    { 94, true, "Kazaja", 10 },
                    { 95, true, "Keniana", 10 },
                    { 96, true, "Kirguisa", 10 },
                    { 97, true, "Kiribatiana", 10 },
                    { 98, true, "Kuwaití", 10 },
                    { 99, true, "Laosiana", 10 },
                    { 100, true, "Lesotense", 10 },
                    { 101, true, "Letona", 10 },
                    { 102, true, "Libanesa", 10 },
                    { 103, true, "Liberiana", 10 },
                    { 104, true, "Libia", 10 },
                    { 105, true, "Liechtensteiniana", 10 },
                    { 106, true, "Lituana", 10 },
                    { 107, true, "Luxemburguesa", 10 },
                    { 108, true, "Macedonia", 10 },
                    { 109, true, "Malgache", 10 },
                    { 110, true, "Malasia", 10 },
                    { 111, true, "Malauí", 10 },
                    { 112, true, "Maldiva", 10 },
                    { 113, true, "Maliense", 10 },
                    { 114, true, "Maltesa", 10 },
                    { 115, true, "Marroquí", 10 },
                    { 116, true, "Mauriciana", 10 },
                    { 117, true, "Mauritana", 10 },
                    { 118, true, "Mexicana", 10 },
                    { 119, true, "Micronesia", 10 },
                    { 120, true, "Moldava", 10 },
                    { 121, true, "Monegasca", 10 },
                    { 122, true, "Mongola", 10 },
                    { 123, true, "Montenegrina", 10 },
                    { 124, true, "Mozambiqueña", 10 },
                    { 125, true, "Birmana", 10 },
                    { 126, true, "Namibia", 10 },
                    { 127, true, "Nauruana", 10 },
                    { 128, true, "Nepalí", 10 },
                    { 129, true, "Nicaragüense", 10 },
                    { 130, true, "Nigerina", 10 },
                    { 131, true, "Nigeriana", 10 },
                    { 132, true, "Noruega", 10 },
                    { 133, true, "Neozelandesa", 10 },
                    { 134, true, "Omaní", 10 },
                    { 135, true, "Neerlandesa", 10 },
                    { 136, true, "Pakistaní", 10 },
                    { 137, true, "Palauana", 10 },
                    { 138, true, "Palestina", 10 },
                    { 139, true, "Panameña", 10 },
                    { 140, true, "Papú", 10 },
                    { 141, true, "Paraguaya", 10 },
                    { 142, true, "Peruana", 10 },
                    { 143, true, "Polaca", 10 },
                    { 144, true, "Portuguesa", 10 },
                    { 145, true, "Británica", 10 },
                    { 146, true, "Centroafricana", 10 },
                    { 147, true, "Checa", 10 },
                    { 148, true, "Congoleña (RDC)", 10 },
                    { 149, true, "Dominicana", 10 },
                    { 150, true, "Ruandesa", 10 },
                    { 151, true, "Rumana", 10 },
                    { 152, true, "Rusa", 10 },
                    { 153, true, "Samoana", 10 },
                    { 154, true, "Sancristobaleña", 10 },
                    { 155, true, "Sanmarinense", 10 },
                    { 156, true, "Sanvicentina", 10 },
                    { 157, true, "Santaluciense", 10 },
                    { 158, true, "Santotomense", 10 },
                    { 159, true, "Senegalesa", 10 },
                    { 160, true, "Serbia", 10 },
                    { 161, true, "Seychellense", 10 },
                    { 162, true, "Sierraleonesa", 10 },
                    { 163, true, "Singapurense", 10 },
                    { 164, true, "Siria", 10 },
                    { 165, true, "Somalí", 10 },
                    { 166, true, "Esrilanquesa", 10 },
                    { 167, true, "Sudafricana", 10 },
                    { 168, true, "Sudanesa", 10 },
                    { 169, true, "Sursudanesa", 10 },
                    { 170, true, "Sueca", 10 },
                    { 171, true, "Suiza", 10 },
                    { 172, true, "Surinamesa", 10 },
                    { 173, true, "Tailandesa", 10 },
                    { 174, true, "Taiwanesa", 10 },
                    { 175, true, "Tanzana", 10 },
                    { 176, true, "Tayika", 10 },
                    { 177, true, "Timorense", 10 },
                    { 178, true, "Togolesa", 10 },
                    { 179, true, "Tongana", 10 },
                    { 180, true, "Trinitense", 10 },
                    { 181, true, "Tunecina", 10 },
                    { 182, true, "Turcomana", 10 },
                    { 183, true, "Turca", 10 },
                    { 184, true, "Tuvaluana", 10 },
                    { 185, true, "Ucraniana", 10 },
                    { 186, true, "Ugandesa", 10 },
                    { 187, true, "Uruguaya", 10 },
                    { 188, true, "Uzbeka", 10 },
                    { 189, true, "Vanuatuense", 10 },
                    { 190, true, "Vaticana", 10 },
                    { 191, true, "Venezolana", 10 },
                    { 192, true, "Vietnamita", 10 },
                    { 193, true, "Yemení", 10 },
                    { 194, true, "Yibutiana", 10 },
                    { 195, true, "Zambiana", 10 },
                    { 196, true, "Zimbabuense", 10 }
                });

            migrationBuilder.InsertData(
                table: "T_PAIS",
                columns: new[] { "IN_COD_PAIS", "BO_ACTIVO_PAIS", "ST_NOMBRE_PAIS", "IN_ORDEN_PAIS" },
                values: new object[,]
                {
                    { 1, true, "Afganistán", 10 },
                    { 2, true, "Albania", 10 },
                    { 3, true, "Alemania", 10 },
                    { 4, true, "Andorra", 10 },
                    { 5, true, "Angola", 10 },
                    { 6, true, "Antigua y Barbuda", 10 },
                    { 7, true, "Arabia Saudita", 10 },
                    { 8, true, "Argelia", 10 },
                    { 9, true, "Argentina", 10 },
                    { 10, true, "Armenia", 10 },
                    { 11, true, "Australia", 10 },
                    { 12, true, "Austria", 10 },
                    { 13, true, "Azerbaiyán", 10 },
                    { 14, true, "Bahamas", 10 },
                    { 15, true, "Bangladés", 10 },
                    { 16, true, "Barbados", 10 },
                    { 17, true, "Baréin", 10 },
                    { 18, true, "Bélgica", 10 },
                    { 19, true, "Belice", 10 },
                    { 20, true, "Benín", 10 },
                    { 21, true, "Bielorrusia", 10 },
                    { 22, true, "Bolivia", 10 },
                    { 23, true, "Bosnia y Herzegovina", 10 },
                    { 24, true, "Botsuana", 10 },
                    { 25, true, "Brasil", 10 },
                    { 26, true, "Brunéi", 10 },
                    { 27, true, "Bulgaria", 10 },
                    { 28, true, "Burkina Faso", 10 },
                    { 29, true, "Burundi", 10 },
                    { 30, true, "Bután", 10 },
                    { 31, true, "Cabo Verde", 10 },
                    { 32, true, "Camboya", 10 },
                    { 33, true, "Camerún", 10 },
                    { 34, true, "Canadá", 10 },
                    { 35, true, "Catar", 10 },
                    { 36, true, "Chad", 10 },
                    { 37, true, "Chile", 0 },
                    { 38, true, "China", 10 },
                    { 39, true, "Chipre", 10 },
                    { 40, true, "Colombia", 10 },
                    { 41, true, "Comoras", 10 },
                    { 42, true, "Congo", 10 },
                    { 43, true, "Corea del Norte", 10 },
                    { 44, true, "Corea del Sur", 10 },
                    { 45, true, "Costa de Marfil", 10 },
                    { 46, true, "Costa Rica", 10 },
                    { 47, true, "Croacia", 10 },
                    { 48, true, "Cuba", 10 },
                    { 49, true, "Dinamarca", 10 },
                    { 50, true, "Dominica", 10 },
                    { 51, true, "Ecuador", 10 },
                    { 52, true, "Egipto", 10 },
                    { 53, true, "El Salvador", 10 },
                    { 54, true, "Emiratos Árabes Unidos", 10 },
                    { 55, true, "Eritrea", 10 },
                    { 56, true, "Eslovaquia", 10 },
                    { 57, true, "Eslovenia", 10 },
                    { 58, true, "España", 10 },
                    { 59, true, "Estados Unidos", 10 },
                    { 60, true, "Estonia", 10 },
                    { 61, true, "Esuatini", 10 },
                    { 62, true, "Etiopía", 10 },
                    { 63, true, "Filipinas", 10 },
                    { 64, true, "Finlandia", 10 },
                    { 65, true, "Fiyi", 10 },
                    { 66, true, "Francia", 10 },
                    { 67, true, "Gabón", 10 },
                    { 68, true, "Gambia", 10 },
                    { 69, true, "Georgia", 10 },
                    { 70, true, "Ghana", 10 },
                    { 71, true, "Granada", 10 },
                    { 72, true, "Grecia", 10 },
                    { 73, true, "Guatemala", 10 },
                    { 74, true, "Guinea", 10 },
                    { 75, true, "Guinea Ecuatorial", 10 },
                    { 76, true, "Guinea-Bisáu", 10 },
                    { 77, true, "Guyana", 10 },
                    { 78, true, "Haití", 10 },
                    { 79, true, "Honduras", 10 },
                    { 80, true, "Hungría", 10 },
                    { 81, true, "India", 10 },
                    { 82, true, "Indonesia", 10 },
                    { 83, true, "Irak", 10 },
                    { 84, true, "Irán", 10 },
                    { 85, true, "Irlanda", 10 },
                    { 86, true, "Islandia", 10 },
                    { 87, true, "Islas Marshall", 10 },
                    { 88, true, "Islas Salomón", 10 },
                    { 89, true, "Israel", 10 },
                    { 90, true, "Italia", 10 },
                    { 91, true, "Jamaica", 10 },
                    { 92, true, "Japón", 10 },
                    { 93, true, "Jordania", 10 },
                    { 94, true, "Kazajistán", 10 },
                    { 95, true, "Kenia", 10 },
                    { 96, true, "Kirguistán", 10 },
                    { 97, true, "Kiribati", 10 },
                    { 98, true, "Kuwait", 10 },
                    { 99, true, "Laos", 10 },
                    { 100, true, "Lesoto", 10 },
                    { 101, true, "Letonia", 10 },
                    { 102, true, "Líbano", 10 },
                    { 103, true, "Liberia", 10 },
                    { 104, true, "Libia", 10 },
                    { 105, true, "Liechtenstein", 10 },
                    { 106, true, "Lituania", 10 },
                    { 107, true, "Luxemburgo", 10 },
                    { 108, true, "Macedonia del Norte", 10 },
                    { 109, true, "Madagascar", 10 },
                    { 110, true, "Malasia", 10 },
                    { 111, true, "Malaui", 10 },
                    { 112, true, "Maldivas", 10 },
                    { 113, true, "Malí", 10 },
                    { 114, true, "Malta", 10 },
                    { 115, true, "Marruecos", 10 },
                    { 116, true, "Mauricio", 10 },
                    { 117, true, "Mauritania", 10 },
                    { 118, true, "México", 10 },
                    { 119, true, "Micronesia", 10 },
                    { 120, true, "Moldavia", 10 },
                    { 121, true, "Mónaco", 10 },
                    { 122, true, "Mongolia", 10 },
                    { 123, true, "Montenegro", 10 },
                    { 124, true, "Mozambique", 10 },
                    { 125, true, "Myanmar", 10 },
                    { 126, true, "Namibia", 10 },
                    { 127, true, "Nauru", 10 },
                    { 128, true, "Nepal", 10 },
                    { 129, true, "Nicaragua", 10 },
                    { 130, true, "Níger", 10 },
                    { 131, true, "Nigeria", 10 },
                    { 132, true, "Noruega", 10 },
                    { 133, true, "Nueva Zelanda", 10 },
                    { 134, true, "Omán", 10 },
                    { 135, true, "Países Bajos", 10 },
                    { 136, true, "Pakistán", 10 },
                    { 137, true, "Palaos", 10 },
                    { 138, true, "Palestina", 10 },
                    { 139, true, "Panamá", 10 },
                    { 140, true, "Papúa Nueva Guinea", 10 },
                    { 141, true, "Paraguay", 10 },
                    { 142, true, "Perú", 10 },
                    { 143, true, "Polonia", 10 },
                    { 144, true, "Portugal", 10 },
                    { 145, true, "Reino Unido", 10 },
                    { 146, true, "República Centroafricana", 10 },
                    { 147, true, "República Checa", 10 },
                    { 148, true, "República Democrática del Congo", 10 },
                    { 149, true, "República Dominicana", 10 },
                    { 150, true, "Ruanda", 10 },
                    { 151, true, "Rumania", 10 },
                    { 152, true, "Rusia", 10 },
                    { 153, true, "Samoa", 10 },
                    { 154, true, "San Cristóbal y Nieves", 10 },
                    { 155, true, "San Marino", 10 },
                    { 156, true, "San Vicente y las Granadinas", 10 },
                    { 157, true, "Santa Lucía", 10 },
                    { 158, true, "Santo Tomé y Príncipe", 10 },
                    { 159, true, "Senegal", 10 },
                    { 160, true, "Serbia", 10 },
                    { 161, true, "Seychelles", 10 },
                    { 162, true, "Sierra Leona", 10 },
                    { 163, true, "Singapur", 10 },
                    { 164, true, "Siria", 10 },
                    { 165, true, "Somalia", 10 },
                    { 166, true, "Sri Lanka", 10 },
                    { 167, true, "Sudáfrica", 10 },
                    { 168, true, "Sudán", 10 },
                    { 169, true, "Sudán del Sur", 10 },
                    { 170, true, "Suecia", 10 },
                    { 171, true, "Suiza", 10 },
                    { 172, true, "Surinam", 10 },
                    { 173, true, "Tailandia", 10 },
                    { 174, true, "Taiwán", 10 },
                    { 175, true, "Tanzania", 10 },
                    { 176, true, "Tayikistán", 10 },
                    { 177, true, "Timor Oriental", 10 },
                    { 178, true, "Togo", 10 },
                    { 179, true, "Tonga", 10 },
                    { 180, true, "Trinidad y Tobago", 10 },
                    { 181, true, "Túnez", 10 },
                    { 182, true, "Turkmenistán", 10 },
                    { 183, true, "Turquía", 10 },
                    { 184, true, "Tuvalu", 10 },
                    { 185, true, "Ucrania", 10 },
                    { 186, true, "Uganda", 10 },
                    { 187, true, "Uruguay", 10 },
                    { 188, true, "Uzbekistán", 10 },
                    { 189, true, "Vanuatu", 10 },
                    { 190, true, "Vaticano", 10 },
                    { 191, true, "Venezuela", 10 },
                    { 192, true, "Vietnam", 10 },
                    { 193, true, "Yemen", 10 },
                    { 194, true, "Yibuti", 10 },
                    { 195, true, "Zambia", 10 },
                    { 196, true, "Zimbabue", 10 }
                });

            migrationBuilder.InsertData(
                table: "T_TIPODOCUMENTO",
                columns: new[] { "IN_COD_TIPODOCUMENTO", "BO_ACTIVO_TIPODOCUMENTO", "ST_NOMBRE_TIPODOCUMENTO", "IN_ORDEN_TIPODOCUMENTO" },
                values: new object[,]
                {
                    { 1, true, "RUT", 1 },
                    { 2, true, "Pasaporte", 2 },
                    { 3, true, "Cédula de identidad extranjera", 3 },
                    { 4, true, "Otro", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_PERSONADECLARADA_IN_COD_NACIONALIDAD",
                table: "T_PERSONADECLARADA",
                column: "IN_COD_NACIONALIDAD");

            migrationBuilder.CreateIndex(
                name: "IX_T_PERSONADECLARADA_IN_COD_PAIS",
                table: "T_PERSONADECLARADA",
                column: "IN_COD_PAIS");

            migrationBuilder.CreateIndex(
                name: "IX_T_PERSONADECLARADA_IN_COD_TIPODOCUMENTO",
                table: "T_PERSONADECLARADA",
                column: "IN_COD_TIPODOCUMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_T_NACIONALIDAD_ST_NOMBRE_NACIONALIDAD",
                table: "T_NACIONALIDAD",
                column: "ST_NOMBRE_NACIONALIDAD",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_PAIS_ST_NOMBRE_PAIS",
                table: "T_PAIS",
                column: "ST_NOMBRE_PAIS",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_T_PERSONADECLARADA_T_NACIONALIDAD_IN_COD_NACIONALIDAD",
                table: "T_PERSONADECLARADA",
                column: "IN_COD_NACIONALIDAD",
                principalTable: "T_NACIONALIDAD",
                principalColumn: "IN_COD_NACIONALIDAD",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_T_PERSONADECLARADA_T_PAIS_IN_COD_PAIS",
                table: "T_PERSONADECLARADA",
                column: "IN_COD_PAIS",
                principalTable: "T_PAIS",
                principalColumn: "IN_COD_PAIS",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_T_PERSONADECLARADA_T_TIPODOCUMENTO_IN_COD_TIPODOCUMENTO",
                table: "T_PERSONADECLARADA",
                column: "IN_COD_TIPODOCUMENTO",
                principalTable: "T_TIPODOCUMENTO",
                principalColumn: "IN_COD_TIPODOCUMENTO",
                onDelete: ReferentialAction.Restrict);

            // Personas ya declaradas: asocia el país en texto libre con T_PAIS
            // (sin distinguir mayúsculas ni tildes). Lo que no calce queda con IN_COD_PAIS NULL
            // y conserva su texto en ST_PAIS_PERSONADECLARADA.
            migrationBuilder.Sql(@"
UPDATE pd
   SET pd.IN_COD_PAIS = p.IN_COD_PAIS
  FROM T_PERSONADECLARADA pd
  JOIN T_PAIS p
    ON p.ST_NOMBRE_PAIS COLLATE Latin1_General_CI_AI
     = LTRIM(RTRIM(pd.ST_PAIS_PERSONADECLARADA)) COLLATE Latin1_General_CI_AI
 WHERE pd.IN_COD_PAIS IS NULL;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Devuelve el país a la columna de texto antes de eliminar T_PAIS
            // (la columna vuelve a ser NOT NULL).
            migrationBuilder.Sql(@"
UPDATE pd
   SET pd.ST_PAIS_PERSONADECLARADA = COALESCE(pd.ST_PAIS_PERSONADECLARADA, p.ST_NOMBRE_PAIS, N'')
  FROM T_PERSONADECLARADA pd
  LEFT JOIN T_PAIS p ON p.IN_COD_PAIS = pd.IN_COD_PAIS
 WHERE pd.ST_PAIS_PERSONADECLARADA IS NULL;");

            migrationBuilder.DropForeignKey(
                name: "FK_T_PERSONADECLARADA_T_NACIONALIDAD_IN_COD_NACIONALIDAD",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_PERSONADECLARADA_T_PAIS_IN_COD_PAIS",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_PERSONADECLARADA_T_TIPODOCUMENTO_IN_COD_TIPODOCUMENTO",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropTable(
                name: "T_NACIONALIDAD");

            migrationBuilder.DropTable(
                name: "T_PAIS");

            migrationBuilder.DropTable(
                name: "T_TIPODOCUMENTO");

            migrationBuilder.DropIndex(
                name: "IX_T_PERSONADECLARADA_IN_COD_NACIONALIDAD",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropIndex(
                name: "IX_T_PERSONADECLARADA_IN_COD_PAIS",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropIndex(
                name: "IX_T_PERSONADECLARADA_IN_COD_TIPODOCUMENTO",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropColumn(
                name: "IN_COD_NACIONALIDAD",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropColumn(
                name: "IN_COD_PAIS",
                table: "T_PERSONADECLARADA");

            migrationBuilder.DropColumn(
                name: "IN_COD_TIPODOCUMENTO",
                table: "T_PERSONADECLARADA");

            migrationBuilder.AlterColumn<string>(
                name: "ST_PAIS_PERSONADECLARADA",
                table: "T_PERSONADECLARADA",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

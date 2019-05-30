import * as fs from "fs";
import * as path from "path";

import { handlebars } from "./handlebarsInst";
import { ife } from "./helpers";
import { EnumSpec } from "./EnumSpec";
import { loadEnum } from "./loadEnum";

const DatasourcePath = path.join( __dirname, "../Datasource" );
const OutputPath = path.join( __dirname, "../Enums.cs" );
const TemplatesPath = path.join( __dirname, "Templates" );
const EnumSpecs: EnumSpec[] = [
    { singleName: "Ability", pluralName: "Abilities" },
    { singleName: "Item", pluralName: "Items" },
    { singleName: "Move", pluralName: "Moves" },
    { singleName: "PokemonType", pluralName: "PokemonTypes" },
    { singleName: "SpeciesType", pluralName: "Species" },
];

function template( name: string ) {
    let templatePath = path.join( TemplatesPath, `${name}.hbs` );

    return fs.readFileSync( templatePath, { encoding: "utf8" } );
}

export function generateEnums() {
    // Initialize the handlebars instance
    handlebars.registerHelper( { ife } );
    handlebars.registerPartial( "enum", template( "enum" ) );

    // Load enum items from data sources
    for ( let enumSpec of EnumSpecs ) {
        loadEnum( enumSpec, DatasourcePath );
    }

    // Render the template
    let mainTemplate = handlebars.compile( template( "enums" ) );
    let sourceCode = mainTemplate( { enums: EnumSpecs } );

    fs.writeFileSync( OutputPath, sourceCode, { encoding: "utf8" } );
}
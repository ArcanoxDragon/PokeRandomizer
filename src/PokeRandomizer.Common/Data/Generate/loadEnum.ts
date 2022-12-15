import * as fs from "fs";
import * as path from "path";

import { EnumSpec } from "./EnumSpec";

export function loadEnum( spec: EnumSpec, cwd: string ) {
	let fileName = path.join( cwd, `${ spec.pluralName }.db` );
	let fileContents = fs.readFileSync( fileName, { encoding: "utf8" } );
	let lineRegex = /^(\d+)=(.*)$/;
	let lines = fileContents.split( /\r?\n/ );

	let items = [];
	let names = [];

	for ( let line of lines ) {
		let match = line.match( lineRegex );

		if ( !match )
			continue;

		let id = +match[ 1 ];
		let name = match[ 2 ];
		let safeName = name;;

		// Clean up name for identifier
		safeName = safeName.replace( /♀/g, "F" );
		safeName = safeName.replace( /♂/g, "M" );
		safeName = safeName.replace( /[^a-zA-Z0-9]+/g, "" );

		if ( /^\d/.test( safeName ) )
		// Can't start with number
			safeName = `_${ safeName }`;

		if ( safeName.trim().length === 0 ) {
			items.push( {
				id: -1,
			} );
			continue;
		}

		names.push( safeName );

		let count = names.filter( n => n === safeName ).length;

		if ( count > 1 ) {
			safeName = `${ safeName }_${ count }`;
		}

		items.push( { id, name, safeName } );
	}

	spec.items = items;
}
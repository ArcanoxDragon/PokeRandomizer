import { HelperOptions } from "handlebars";

const Operators = {
    "<": ( l, r ) => l < r,
    "<=": ( l, r ) => l <= r,
    ">": ( l, r ) => l > r,
    ">=": ( l, r ) => l >= r,
    "==": ( l, r ) => l == r,
    "!=": ( l, r ) => l != r,
};

export function ife( left: any, op: keyof typeof Operators, right: any, options: HelperOptions ) {
    let opFunc = Operators[ op ];

    if ( typeof opFunc !== "function" )
        throw new Error( "Error: invalid operand \"" + op + "\"" );

    if ( opFunc( left, right ) ) {
        return options.fn( this );
    } else {
        return options.inverse( this );
    }
}
import { generateEnums } from "./Data/Generate/generateEnums";

export default function ( done ) {
    generateEnums();

    done();
}
import { DataItem } from "./DataItem";

export interface EnumSpec {
    singleName: string;
    pluralName: string;
    items?: DataItem[];
}
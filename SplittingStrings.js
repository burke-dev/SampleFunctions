function splitValueByDelimiter(value) {
  if(typeof value !== 'string') {
    throw new Error(`splitValueByDelimiter() value type must be string\r\ntypeof value === ${typeof value}`);
  }
  /*
    If a delimiter in the array is entered then this will replace them with a comma then split on each comma returning an array of strings to be used in other functions.
    This was developed initially for lists of skills or certifications to be listed off on a resume, but since some will want them displayed as bullets while others
    will have them listed with commas or slashes between them, getting this into a consistent state allows this data to be used easily and consistently.
    This also filters out empty results for consistency and corrects for human error.
  */
  [";", "/", "-"].map(delimiter => value = value.replace(new RegExp(delimiter, 'gi'), ","));
  return value.split(",").filter(x => x);
}

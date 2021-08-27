function splitValueByDelimiter(value) {
  if(typeof value !== 'string') {
    console.error(`_splitValuesByDelimiter value type must be string\r\nvalue's typeof ${typeof value}\r\n${value.toString()}`);
    return null;
  }

  const delimitersUsed = [",", ";", "/", "-"].filter(x => {
    if(value.includes(x)) {
      return x;
    }
  });
 
  /*
    If a listed delimiter is entered then this will replace them with a comma then split on each comma returning an array of strings to be used in other functions.
    This was developed initially for lists of skills or certifications to be listed off on a resume, but since some will want them displayed as bullets while others
    will have them listed with commas or slashes between them, getting this into a consistent state allows this data to be used easily and consistently.
    This also filters out empty results for consistency and corrects for human error.
  */
  if(delimitersUsed.length) {
    delimitersUsed.map(x => value = value.replace(new RegExp(x, 'gi'), ","));
    return value.split(",").filter(x => x);
  }
  // If no delimiters are used, this simply returns as an array with one entry
  return [value];
}

function splitValueByMultipleDelimiters(value) {
  try {
    /*
      The `value` string entered will be checked for the delimiters `;/-` and replace them with a comma.
      Then split on each comma returning an array of strings to be used in other functions.
      This was developed initially for lists of skills or certifications to be listed off on a resume, but since some will want them displayed as bullets while others
      will have them listed with commas or slashes between them, getting this into a consistent state allows this data to be used easily and consistently.
      This also filters out empty results for consistency and corrects for human error.
      TODO - add an escape option to allow delimiters to not be replaced/split on.
    */
    return value.split(/\s*[;\-/,]\s*/g).filter(x => x);
  }
  catch(err) {
    throw new Error(`splitValueByMultipleDelimiters() value type must be string\r\ntypeof value === ${typeof value}\r\n${err}`);
  }
}

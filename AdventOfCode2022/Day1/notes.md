Using an unparsable int as a delineator was a hack.
It would have been better to check for the newline.
Junk data should cause an exception, not incorrect results.
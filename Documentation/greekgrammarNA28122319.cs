<IDictionary>
"..\GreekLogic1904\bin\debug\GreekLogic.dll"

<IScanner>
"..\GreekLogic1904\bin\debug\GreekLogic.dll"

<Grammar>

// AUTOMATIC PROJECTION RULES (TERMINAL NODES)
/* EXPLANATORY NOTE: PROJECTION RULES PRESCRIBE NORMAL FUNCTION PROJECTING UPWARDS;
					 TRANSFORMATION RULES CORRECT MORPH ERRORS OR FORM-FUNCTION MISMATCHES*/

// Nouns promoted to np unless N2NP rule blocked in 0.BR
[N2NP] noun* -> np
	? { ! ( "N2NP" in 0.BR ) }

[Num2Nump] num* -> nump
	? { ! ( "Num2Nump" in 0.BR ) }

/* Pronouns promoted to np  
Automatic promotion unless Pron2NP rule blocked in 0.BR
blocking disabled in the case of the listed lemmas
("I", "you", "he", "this", "that", "my own", "your own", "his/her/its own")
*/
[Pron2NP] pron* -> np
	? { ! ( "Pron2NP" in 0.BR )
		|| 0.Lemma in {"e)gw/", "su/", "au)to/s", "ou(=tos", "e)kei=nos", "e)mautou=", "seautou=", "e(autou="}
		|| 0.UnicodeLemma in {"ἐγώ","ἐγώ", "σύ","σύ", "αὐτός","αὐτός", "οὗτος", "ἐκεῖνος", "ἐμαυτοῦ", "σεαυτοῦ", "ἑαυτοῦ"} // both "ἐγώ","ἐγώ", both "σύ","σύ", & both "αὐτός","αὐτός" used because of font differences
	  }

/* Verbs promoted to vp unless V2VP rule blocked in 0.BR; 
participles always promoted to vp even when V2VP rule blocked in 0.BR
*/
[V2VP] verb* -> vp
	? { ! ( "V2VP" in 0.BR ) 
		&& ! ( 0.UnicodeLemma in {"ἰδού","ἴδε"} ) // Logos morph has as verb rather than ptcl
		|| "V2VPx" in 0.AR
		|| 0.Mood=="Participle"
	  }

// Randall 04/14/11 Rom11:22 for JT morph
[Intj2VP] intj* -> vp
    ? { "Intj2VP" in 0.AR }

[V2Intj] verb* -> intj
	? { 0.UnicodeLemma in {"ἰδού","ἴδε"}  // Logos morph has as verb rather than ptcl
		&& ! ( "V2Intj" in 0.BR )
		&& ! ( "V2VPx" in 0.AR )
	  }

// Adjectives promoted to adjp unless Adj2Adj rule blocked in 0.BR
[Adj2Adjp] adj* -> adjp
	? { ! ( "Adj2AdjP" in 0.BR || "Adj2Adjp" in 0.BR ) }

// Adverbs promoted to advp unless Adv2Conj rule applied in 0.AR
[Adv2Advp] adv* -> advp
	? { ( ! ( "Adv2Conj" in 0.AR )
		&& ! ( 0.UnicodeLemma in {"ἐπάνω"} && 0.functionMorph == "P" )
        && ! ( 0.Lemma in {"ἵνα"}
             && 0.Cat in {"adv"} )
        )
		|| ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR )
	  }
//	? { ! ( "Adv2Advp" in 0.BR ) }		

// Logos morph has a number of adverbs functioning as prep simply as adv (divide into three listings as too long)
[Adv2Prep] adv* -> prep
	? { ( 0.UnicodeLemma in {"ἅμα","ἀπέναντι","ἀπέναντι","ἄντικρυς","ἀντιπέρα","ἐγγύς","ἐκτός","ἔμπροσθεν","ἔναντι","ἐναντίον","ἕνεκα","ἐντός"} // "ἀπέναντι" added because this erroneous forrm in SBLGNT
		&& 0.functionMorph == "P"
		&& ! ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR || "advp2np" in 0.AR )
        )
		|| ( 0.UnicodeLemma in {"ἐνώπιον","ἔξω","ἔξωθεν","ἐπάνω","ἐπάνω" /*SBL error*/ ,"ἐπέκεινα","ὑπερέκεινα","ἔσω","κατέναντι","κύκλῳ","κυκλόθεν","μέχρι"}
		&& 0.functionMorph == "P" 
		&& ! ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR || "advp2np" in 0.AR )
        )
		|| ( 0.UnicodeLemma in {"μεταξύ","ὄπισθεν","ὀπίσω","παρεκτός","πέραν","πλήν","πλησίον","ὑπεράνω","ὑποκάτω","χάριν","χωρίς"}
		&& 0.functionMorph == "P"
		&& ! ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR || "advp2np" in 0.AR )
        )
        || 0.Lemma in {"ἵνα"}
           && 0.Cat in {"adv"}
        || "Adv2Prep" in 0.AR
      }

[pp2ADV] pp* -> ADV
	? { ! ( "pp2PP" in 0.BR || "pp2ADV" in 0.BR ) 
//	  && ! ( "pp2P" in 0.AR )
	  }
	>> { .PrepLemma = ""; }


// AUTOMATIC PROJECTION RULES (NON-TERMINAL NODES)

[Advp2ADV] advp* -> ADV
	? { ! ( "advp2ADV" in 0.BR || "Advp2ADV" in 0.BR )  
//	  && ! ( 0.Lemma in {"ou)de/","ou)","mh/"} )
//	  && ! ( 0.UnicodeLemma in {"οὐδέ","οὐδέ","οὐ","μή","μή"} ) // "μή"and "μή" used throughout because of font differences among texts (SBLGNT & AF, apparently)
      && ! ( "advp2P" in 0.AR )
	  && ! ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR )
      && ! ( 0.Case == "Dative"
	       && ( "IONp" in 0.AR || "DativeNp" in 0.AR )
            )
      && ! ( "np2advp" in 0.AR 
             && 0.Case == "Dative"
            )
	  }
	>> { if ( 0.Lemma in {"ou)","ou)de/","ou)dei/s","ou)de/pote","ou)ke/ti","ou)/pw","ou)/te","mh/","mhdamw=s","mhde/","mhdei/s","mhqamw=s","mhke/ti","mh/te"} ) .Neg = true;
         if ( 0.UnicodeLemma in {"οὐ","οὐδέ","οὐδέ","οὐδείς","οὐδέποτε","οὐθείς","οὐκέτι","οὔπω","οὔτε","μή","μή","μηδαμῶς","μηδέ","μηδείς","μηθαμῶς","μηκέτι","μήτε"} ) .Neg = true; }

[Vp2V] vp* -> V
    ?  { ( ! ( 0.UnicodeLemma in {"εἰμί","εἰμί"} ) //JT morph & SBL morph font are different & both are required
         && ! ( "vp2V" in 0.BR || "Vp2V" in 0.BR )
         )
       || ( "vp2V" in 0.AR || "Vp2V" in 0.AR )
       }

[vp2VC] vp* -> VC
	? { ( 0.UnicodeLemma in {"εἰμί","εἰμί","γίνομαι","γίνομαι","γίγνομαι"} // "be" "become"; JT morph & SBL morph font are different & all are required
	    && ! ( "vp2VC" in 0.BR )
	    )
	  || "vp2VC" in 0.AR
	  }

[Adjp2P] adjp* -> P
	? { ! ( "adjp2P" in 0.BR || "Adjp2P" in 0.BR ) 
	  && 0.Case != "Vocative" 
	  }

[Nump2NP] nump* -> np
	? { ( "NP-PP" in 0.AR || "NpPp" in 0.AR )
		|| ( "NP-Appos" in 0.AR || "Np-Appos" in 0.AR ) 
		|| "adjp2ADV" in 0.AR // Logos morph as num, when num functioning as ADV in place of elided np
		|| "Adj2NP" in 0.AR
		|| 0.Substantive
		|| "Nump2NP" in 0.AR
		|| ( "np2O" in 0.AR || "Np2O" in 0.AR )
		|| 0.Rule in {"AdvpNump"}
	  }

[pp2P] pp* -> P
	? { ! ( "pp2P" in 0.BR ) }


// SINGLE NODE CONDITIONAL AUTOMATIC TRANSFORMATION RULES

/* Adjp promoted to np,
Note: New addition to make former AdjpofNp go to NPofNP involves promoting adj to np when
followed by genitive, where adjp not result of CL2Adjp rule
and not one of the listed lemma
and not followed by a noun with same case, gender, and number
and not preceded by a noun with same case, gender, and number
(the above keep AdjpNp and NpAdjp cases from having extra trees).
When in nominative case in first node,
when in genitive with a prep one node before it,
when in accusative with a genitive one node before it 
and a verb one node after it,
unless there is a determiner one node before it,
or a noun one node after it,
or Adj2Advp, adjp2ADV, or AdjpofNP rule applied,
or Adj2NP blocked,
or rule used is CL2Adjp,
Adjp also promoted to np when node category is substantive,
or when Adj2NP applied
*/
[Adj2NP] adjp* -> np
	? { ( ( 0.Case == "Nominative" 
		   || ( 0.Case == "Genitive" && 0.Start > 0 && "prep" in tokens[0.Start-1,0.Start-1].Cat )
		   || ( 0.Case == "Accusative" 
		      && ( 0.Start > 0 &&  "Genitive" in tokens[0.Start-1,0.Start-1].Case )
		      && 0.End < tokens.Count-1 && "verb" in tokens[0.End+1,0.End+1].Cat 
		      )
		   )
	    && ! ( 0.Start > 0 && "det" in tokens[0.Start-1,0.Start-1].Cat )
	    && ! ( 0.End < tokens.Count-1 && "noun" in tokens[0.End+1,0.End+1].Cat ) 
	    && ! ( "Adj2Advp" in 0.AR )
	    && ! ( "adjp2ADV" in 0.AR )
   	    && ! ( "AdjpOfNP" in 0.AR || "AdjpofNp" in 0.AR )
	    && ! ( "Adj2NP" in 0.BR )
	    && ! ( 0.Rule in {"CL2Adjp"} )
	    )
	  || ( 0.End < tokens.Count-1 && "Genitive" in tokens[0.End+1,0.End+1].Case
		   && ! ( 0.Rule in {"CL2Adjp"} )
		   && ! ( 0.Lemma in {"me/sos","mesto/s","a)/xios", "e)/nocos", "plh/rhs", "me/gas", "prw=tos", "ta/cion", "cei/rwn", "a)kata/paustos"} 
                  || 0.UnicodeLemma in {"μέσος","μεστός","ἄξιος", "ἔνοχος", "πλήρης", "μέγας", "πρῶτος", "ταχέως", "χείρων", "ἀκατάπαυστος"} 
                )
		   && ! ( 0.End < tokens.Count-1
				&& "noun" in tokens[0.End+1,0.End+1].Cat
				&& 0.Case in tokens[0.End+1,0.End+1].Case
				&& 0.Gender in tokens[0.End+1,0.End+1].Gender
				&& 0.Number in tokens[0.End+1,0.End+1].Number
				)
		   && ! ( 0.Start > 0
				&& "noun" in tokens[0.Start-1,0.Start-1].Cat
				&& 0.Case in tokens[0.Start-1,0.Start-1].Case
				&& 0.Gender in tokens[0.Start-1,0.Start-1].Gender
				&& 0.Number in tokens[0.Start-1,0.Start-1].Number
				)
		   && ! ( "Adj2Advp" in 0.AR )
		   && ! ( "adjp2ADV" in 0.AR )
   		   && ! ( "AdjpOfNP" in 0.AR || "AdjpofNp" in 0.AR )
   		   && ! ( "AdjNP" in 0.AR )
	       && ! ( "Adj2NP" in 0.BR )
	       && ! ( 0.Rule in {"CL2Adjp"} )
		 )  
	  || 0.Substantive
	  	 && ! ( "Adj2NP" in 0.BR )
	  || "Adj2NP" in 0.AR
	  || ( "np2O2" in 0.AR || "Np2O2" in 0.AR ) // phm1:17 with Logos morph adj for what used to be noun
	  || 0.UnicodeLemma in {"χιλιάς","χιλιάς"}
	  && ! ( "AdjpNP" in 0.BR || "AdjpNp" in 0.BR )
	  }

// Converts pron to adj when pron2adj applied		
/* 
When the listed lemma co-occur with a noun in matching case,
gender, and number one word before or one word after,
unless pron2adj is blocked or it is the Acc Neut Sing form
(this rules out a very large number of false positives with
the interrogative "what" use of "ti/s", even though it then
makes it necessary to manually reapply in some cases of Acc Neut
Sing form where it should have gone with pron2adj)
*/
[pron2adj] pron* -> adj
	? { "pron2adj" in 0.AR
	  || ( ( 0.Lemma in {"ti/s", "poi=os", "tosou=tos"} || 0.UnicodeLemma in {"τίς","τις","τὶς","τίς","ποῖος","τοσοῦτος"} ) // "τὶς" added because SBLGNT has this erroneous form
            && ( 0.Start > 1 && ( "noun" in tokens[0.Start-1,0.Start-1].Cat || ( true in tokens[0.Start-1,0.Start-1].Substantive && ! ( "NPofNP" in 0.AR )  ) )
		         && 0.Case in tokens[0.Start-1,0.Start-1].Case
		         && 0.Gender in tokens[0.Start-1,0.Start-1].Gender
		         && 0.Number in tokens[0.Start-1,0.Start-1].Number
                )
		 && ! ( "pron2adj" in 0.BR )
		 && ! ( 0.Case=="Accusative"
				&& 0.Gender=="Neuter"
				&& 0.Number=="Singular"
			  )	
		 )
	  || ( ( 0.Lemma in {"ti/s", "poi=os", "tosou=tos"} || 0.UnicodeLemma in {"τίς","τις","τὶς","τίς","ποῖος","τοσοῦτος"} )
		 && ( 0.End < tokens.Count-1 && ( "noun" in tokens[0.End+1,0.End+1].Cat || ( true in tokens[0.End+1,0.End+1].Substantive && ! ( "NPofNP" in 0.AR ) ) )
		    && 0.Case in tokens[0.End+1,0.End+1].Case
		    && 0.Gender in tokens[0.End+1,0.End+1].Gender
		    && 0.Number in tokens[0.End+1,0.End+1].Number
            )
		 && ! ( "pron2adj" in 0.BR )
		 && ! ( 0.Case=="Accusative"
				&& 0.Gender=="Neuter"
				&& 0.Number=="Singular"
			  )		 
		 )
	  || 0.UnicodeLemma in {"σός","ἐμός","ἡμέτερος","ὑμέτερος"} // where Logos morph has so/s as pron where adjectival pron
		 && ! ( 0.Start > 1 && "det" in tokens[0.Start-1,0.Start-1].Cat && 0.End==0.Start 
				&& ! ( 0.End < tokens.Count-1 && "noun" in tokens[0.End+1,0.End+1].Cat
					 && 0.Case in tokens[0.End+1,0.End+1].Case
				     && 0.Gender in tokens[0.End+1,0.End+1].Gender
					 && 0.Number in tokens[0.End+1,0.End+1].Number
					 )
			  )
	  }


// SINGLE NODE CONDITIONAL AUTOMATIC PROJECTION RULES

[Np2S] np* -> S
	? { ( 0.Case == "Nominative" 
//	    && !( 0.Relative || 0.Type=="Relative")
	    && ! ( "np2S" in 0.BR || "Np2S" in 0.BR )
	    && ! ( "np2ADV" in 0.AR || "Np2ADV" in 0.AR )
	    && ! ( "np2P" in 0.AR || "Np2P" in 0.AR )
	    )
	  || ( "np2S" in 0.AR || "Np2S" in 0.AR )
      || "CL2S" in 0.AR
         && 0.Mood in {"Infinitive"}
	  }

[Np2O] np* -> O
	? { ( 0.Case == "Accusative" 
//	    && !( 0.Relative || 0.Type=="Relative")
	    && ! ( "np2O" in 0.BR || "Np2O" in 0.BR )
	    && ! ( "np2ADV" in 0.AR || "Np2ADV" in 0.AR )
	    && ! ( "np2O" in 0.AR || "Np2O" in 0.AR )
	    )
	  || ( "np2O" in 0.AR || "Np2O" in 0.AR )
	  }
	
[Np2IO] np* -> IO
	? { ( 0.Case == "Dative" 
//	    && !( 0.Relative || 0.Type=="Relative")
	    && ! ( "np2IO" in 0.BR || "Np2IO" in 0.BR )
	    && ! ( "np2O" in 0.AR || "Np2O" in 0.AR )
	    && ! ( "np2ADV" in 0.AR || "Np2ADV" in 0.AR )
	    )
	  || ( "np2IO" in 0.AR || "Np2IO" in 0.AR )
	  }

[Np2P] np* -> P
	? { ( 0.Case in {"Nominative"}
	    && ! ( 0.Rule in {"CL2NP","Adj2NP"} 
             || ( 0.Rule in {"Pron2NP"}
                && ! ( 0.Type in {"Indefinite"} )
                )
             )
	    && ! ( "np2P" in 0.BR || "Np2P" in 0.BR )
	    && ! ( "np2S" in 0.AR || "Np2S" in 0.AR )
//	    && ! ( 0.Mood in {"Participle","Infinitive"} )
	    )
      || ( "np2P" in 0.AR || "Np2P" in 0.AR )
      || ( ( 0.Interrogative || 0.Type=="Interrogative") && ! ( "np2P" in 0.BR || "Np2P" in 0.BR ) )
	  || "advp2P" in 0.AR	// Logos morph has adverbial "where" relative pronoun as pron
		 && 0.functionMorph == "B"
	  }

// # Consider putting a)mh/n, where not functioning as ADV, into automatic Ptcl2Intj & Intj2CL
[Ptcl2Intj] ptcl* -> intj
	? { "Ptcl2Intj" in 0.AR
		&& ! ( 0.UnicodeLemma in {"οὐ","οὐ"} )
		|| 0.Lemma in { "i)dou/", "i)/de" }		
		|| 0.UnicodeLemma in {"ἰδού","ἴδε"}
		|| 0.UnicodeLemma in {"ναί","ναί"}
		   && "Ptcl2Np" in 0.AR
		   && ( "np2CL" in 0.AR || "Np2CL" in 0.AR )
	  }


// CONDITIONAL AUTOMATIC CLAUSE EMBEDDING PROJECTION
		  
// CL functioning as np
[CL2NP] CL* -> np
    ? { ( !0.Relativized && 0.Type=="Relative"
        && ! ( "CL2NP" in 0.BR )
		&& ! ( 0.Rule in { "Np2CL","Conj-CL" } )
		&& ! ( 0.Start > 1 
 			 && "np" in tokens[0.Start-1,0.Start-1].Cat
			 && "NP-CL" in tokens[0.Start-1,0.Start-1].AR
			 )
	    )
	  || "CL2NP" in 0.AR
		 && ! ( 0.Rule in { "Np2CL" } )
      || "CL2S" in 0.AR
         && 0.Mood in {"Infinitive"}
   	  }
    >> { if (0.Type=="Relative") .Relativized= true; }

[Vp2Np] vp* -> np
	? { "vp2NP" in 0.AR 
	  || "VP2NP" in 0.AR
	  || "Vp2Np" in 0.AR
	  }
	  
[CL2ADV] CL* -> ADV
	? { ( 0.Mood in {"Participle","Infinitive"}
	    && ! ( 0.Rule in {"PP-V-PP"} )
	    && ! ( "CL2O" in 0.AR )
	    && ! ( "CL2S" in 0.AR )
	    && ! ( "CL2P" in 0.AR )
		&& ! ( "ADV2CL" in 0.AR)
	    && ! ( "vp2ADV" in 0.BR || "CL2ADV" in 0.BR )
	    )
	  || ( "vp2ADV" in 0.AR || "CL2ADV" in 0.AR )
	  }


// MANUAL CLAUSE EMBEDDING PROJECTION RULES
	  
// CL functioning as adjp
/* Php2:14 is incontrovertible evidence of participle as adjp;
so participial CL allowed to be adjp in relation to np; when
participial CL has det, then DetCL and Np-Appos in relation to np
however, this has been only partially applied--it may be best to 
stick with simply NP-CL and CL-NP (depending on the relative position
of the participial clause to the np it modifies); but allowed CL2Adjp
to stand in Matthew's work--see if it ends up looking better that way? #
*/
[CL2Adjp] CL* -> adjp
	? {  "CL2Adjp" in 0.AR 
		 && ! ( 0.Rule in { "Conj-CL" } )
		 || "V2Adjp" in 0.AR
		 && ! ( 0.Rule in { "Conj-CL" } )
   	  }

// CL functioning as vp
[CL2VP] CL* -> vp
	? { "CL2VP" in 0.AR 
	  && !0.cl2vp
	  }
	>> { .cl2vp = true; }

// CL functioning as S	
[CL2S] CL* -> S
	? { "CL2S" in 0.AR
        && ! ( 0.Mood in {"Infinitive"} )
      }

// CL functioning as O	
[CL2Ox] CL* -> O
	? { "CL2O" in 0.AR || "CL2Ox" in 0.AR }

// CL functioning as O2	
[CL2O2x] CL* -> O2
	? { "CL2O2" in 0.AR || "CL2O2x" in 0.AR }

// CL functioning as P	
[CL2P] CL* -> P
	? { "CL2P" in 0.AR 
	  && ! ( 0.Rule in {"VC-ADV-P","P2CL"} )
	  }


// SINGLE NODE MANUAL PROJECTION RULES

[adjp2S] adjp* -> S
	? { "adjp2S" in 0.AR }	
	
[Np2O2] np* -> O2
	? { "np2O2" in 0.AR || "Np2O2" in 0.AR }
	
[Np2ADV] np* -> ADV
	? { ( "np2ADV" in 0.AR || "Np2ADV" in 0.AR )  
		|| 0.Rule == "Nump2NP" // Logos morph as num, when num functioning as ADV in place of elided np
		   && "adjp2ADV" in 0.AR
		|| 0.UnicodeLemma in {"ἀκμήν"}
		   && 0.functionMorph=="B"
	  }

[Np2pp] np* -> pp
	? { "np2pp" in 0.AR || "Np2pp" in 0.AR }

// Randall 10/5/09 act26:8:1
[Adjp2O] adjp* -> O
	? { "adjp2O" in 0.AR || "Adjp2O" in 0.AR }
	
[adjp2O2] adjp* -> O2
	? { "adjp2O2" in 0.AR }
	
[adjp2ADV] adjp* -> ADV
	? { "adjp2ADV" in 0.AR }

// # check to see if there is consistency in when Adj2Advp, adjp2ADV, and adjp2advp is used #	
[adjp2advp] adjp* -> advp
	? { "adjp2advp" in 0.AR }
       
[Vp2P] vp* -> P
	? { "vp2P" in 0.AR || "Vp2P" in 0.AR }
	
[Pp2np] pp* -> np
	? { "pp2np" in 0.AR || "Pp2np" in 0.AR }
	
[Advp2P] advp* -> P
	? { ( "advp2P" in 0.AR || "Advp2P" in 0.AR )
		|| ( "adjp2P" in 0.AR || "Adjp2P" in 0.AR )
		   && 0.functionMorph == "J" // where Logos morph has adv when old morph had adj
		   && ! ( "advp2P" in 0.BR || "Advp2P" in 0.BR )
	  }	

[advp2V] advp* -> V
	? { "advp2V" in 0.AR }

[advp2np] advp* -> np
	? { "advp2np" in 0.AR }
	
[advp2pp] advp* -> pp
	? { "advp2pp" in 0.AR }

[Ptcl2Np] ptcl* -> np
	? { "Ptcl2Np" in 0.AR 
		&& ! ( 0.UnicodeLemma in { "ἰδού","ἴδε","ἰδού","ἴδε" } ) // 2 sets to address both JT morph & SBL morph
	  }
	
[Ptcl2Adv] ptcl* -> adv
	? { "Ptcl2Adv" in 0.AR
		&& ! ( 0.UnicodeLemma in {"ἀμήν","ἀμήν"} ) // to block and leave only automatic PtclCL for a)mh/n
		|| "Ptcl2Intj" in 0.AR
		   && ( 0.Lemma in {"ou)"} || 0.UnicodeLemma in {"οὐ"} )
		|| ( (0.Lemma in {"ou)","mh/"} || 0.UnicodeLemma in { "οὐ","μή","μή","οὐχί" } ) // οὐχί is put under ou) in the lemma
		   && ! ( "PtclCL" in 0.AR )
           && ! ( "Ptcl2Adv" in 0.BR ) // addresses 1co6:10 46006010007 SBLGNT & JT morph conflict
		   && ! ( "that-VP" in 0.AR || "sub-CL" in 0.AR )
		   )
		|| 0.UnicodeLemma in { "καί" } // to deal with Logos morph having adverbial KAI as ptcl
		|| ( "AdvADJP" in 0.AR || "AdvpAdjp" in 0.AR )
		|| 0.UnicodeLemma in {"ποτέ"} // to deal with Logos morph having pote/ as ptcl (e.g., gal1:23)
		   && 0.functionMorph=="BX"
		|| "ADV2CL" in 0.AR // luk20:21
	  }

[Intj2V] intj* -> V
	? { "Intj2V" in 0.AR
//		|| 0.Lemma in { "i)dou/", "i)/de" }
//      || 0.UnicodeLemma in { "ἰδού","ἴδε" }
		&& ! ( 0.UnicodeLemma in { "ἰδού","ἴδε","ἰδού","ἴδε" } )
	  }


// SINGLE NODE MANUAL TRANSFORMATION RULES

// Converts adj to advp when Adj2Advp rule applied in 0.AR
[Adj2Advp] adj* -> advp
	? { "Adj2Advp" in 0.AR 
//	  || 0.Lemma in {"eu)qu/s"} // "immediately"
//	  || 0.UnicodeLemma in {"εὐθύς"} // "immediately"
	  }

// Converts determiner to np when Det2NP rule applied in 0.AR 	  
[Det2NP] det* -> np
/*	? { ( ! ( 0.End < tokens.Count-1 && "noun" in tokens[0.End+1,0.End+1].Cat )
	    && ! ( "Det2NP" in 0.BR )
	    )
	  || "Det2NP" in 0.AR  
	  } */
	? { "Det2NP" in 0.AR }

// Converts conj to prep when Conj2Prep applied
[Conj2Prep] conj* -> prep
	? { "Conj2Prep" in 0.AR 
		|| 0.functionMorph == "P" // when Logos morph has conj for hEWS and others used as prep
        || 0.UnicodeLemma in {"ὡς","ὡσεί","ὡσεί"}
           && "Conj2Adv" in 0.AR
           && ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR )
	  }

// Randall 10/6/09 rev7:2:1-7:3:22
[Prep2Conj] prep* -> conj
	? { 0.UnicodeLemma in {"ἄχρι"}
		&& 0.functionMorph == "C"
		&& "sub-CL" in 0.AR
        || "Prep2Conjx" in 0.AR // 1co14:5 SBLGNT
	  }

// Randall 9/30/09 2co11:23
[Prep2Adv] prep* -> adv
	? { "Prep2Adv" in 0.AR }

// Converts conj to adv when Conj2Adv applied
[Conj2Adv] conj* -> adv
	? { ( "Conj2Adv" in 0.AR
        && ! ( 0.UnicodeLemma in {"ὡς","ὡσεί","ὡσεί"}
                 && "Conj2Adv" in 0.AR
                && ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR )              ) 
        )
		|| 0.Type == "Logical" && ( 0.SubType == "Ascensive" || 0.SubType == "Adverbial" )
		|| "AdvpCL" in 0.AR
		|| "notCLbutCL2CL" in 0.AR // when Logos morph has conj for OUDE
		|| ( 0.UnicodeLemma in {"ὅπου"} // when Logos morph has conj for o(/pou when conjunctive adverb
		   && 0.functionMorph == "B" )
		|| ( 0.UnicodeLemma in {"καί"} // when Logos morph has conj for ascensive kai/ (e.g., gal2:13) 
		   && 0.functionMorph == "B"
		   && ( "advp2ADV" in 0.BR || "Advp2ADV" in 0.BR )
           && ! ( "Conj2Adv" in 0.BR ) // mat23:32 Logos morph & JT parsing clash
           ) 
		|| ( 0.UnicodeLemma in {"οὐ"} // when Logos morph has conj for ou) (e.g., gal3:12:4)
		   && 0.functionMorph=="B"
		   && ! ( "Adv2Conj" in 0.AR ) // avoid overflowing stack in gal1:9:3
           )
		|| ( 0.UnicodeLemma in {"πόθεν"} // jhn9:29
		   && 0.functionMorph=="B"
		   && ( "advp2P" in 0.AR || "Advp2P" in 0.AR )
           )
		|| ( ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR )
           && ! ( 0.UnicodeLemma in {"ὡς","ὡσεί","ὡσεί"}
                 && "Conj2Adv" in 0.AR
                )
            )
		|| ( "AdvADJP" in 0.AR || "AdvpAdjp" in 0.AR )
		|| ( "AdvpAdvp" in 0.AR || "2Advp_h1" in 0.AR ) && 0.functionMorph=="B" // 1th2:10
		|| ( "AdvPP" in 0.AR || "AdvPp" in 0.AR )&& 0.functionMorph=="B" // 2th2:1
        || ( 0.NA27Unicode in {"μήποτε","μήποτέ"}
             && 0.Lemma in {"πότε"}
           )
	  }

//Randall 8/30/08 for act23:9:23
[Conj2Ptcl] conj* -> ptcl
	? { "Conj2Ptcl" in 0.AR
		|| 0.Type == "Logical" && ( 0.SubType == "Ascensive" || 0.SubType == "Adverbial" )
		|| "PtclCL" in 0.AR
		|| (0.Lemma in {"mh/"} || 0.UnicodeLemma in {"μή","μή"} )
		   && ( "that-VP" in 0.AR || "sub-CL" in 0.AR ) // convert mh/ introducing clauses from conj to ptcl
	  }

// Converts conj to pron when Conj2Pron applied
[Conj2Pron] conj* -> pron
	? { "Conj2Pron" in 0.AR
		|| 0.UnicodeLemma in {"κἀγώ"} // Logos morph has conj rather than adv for ka)gw/ (which needs to be pron)
	  }

// Accounts for when Logos morph has ptcl for ka)gw/
[Ptcl2Pron] ptcl* -> pron
	? { "Ptcl2Pron" in 0.AR
		|| 0.UnicodeLemma in {"κἀκεῖνος","κἀγώ"}
		   && ( "advp2np" in 0.AR || 0.functionMorph=="BE" )
	  }

// Converts adv to conj when Adv2Conj applied
[Adv2Conj] adv* -> conj
	? { "Adv2Conj" in 0.AR }

// Converts adv to ptcl when Adv2Ptcl applied
[Adv2Ptcl] adv* -> ptcl
	? { "PtclCL" in 0.AR 
		&& ! ( "Adv2Ptcl" in 0.BR ) // gal4:11:1-4:11:8
	  }

//Randall 11/17/08 for 1Co5:3
[Ptcl2Conj] ptcl* -> conj
	? { "Ptcl2Conj" in 0.AR 
		|| 0.UnicodeLemma in {"μέν","μήτε","οὔτε"} // Logos morph has ptcl rather than conj for me/n when correlative with de/
		   && ( "EitherOrCL" in 0.AR || "aCLaCL" in 0.AR || "EitherOr3CL" in 0.AR || "aCLaCLaCL" in 0.AR || "EitherOr4CL" in 0.AR || "EitherOrVp" in 0.AR || "EitherOrAdjp" in 0.AR
			  || "EitherOrNp" in 0.AR || "aNpaNp" in 0.AR || "EitherOrPp" in 0.AR  || "aPpaPp" in 0.AR|| "EitherOr3Np" in 0.AR|| "aNpaNpaNp" in 0.AR || "EitherOr4Np" in 0.AR || "EitherOr5Np" in 0.AR
			  || "EitherOr8Np" in 0.AR || "EitherOr3Vp" in 0.AR || "EitherOr4Vp" in 0.AR || "EitherOr5Vp" in 0.AR || "EitherOr3Pp" in 0.AR || "aPpaPpaPp" in 0.AR
			  || "EitherOr4Pp" in 0.AR || "EitherAdvpOrPp" in 0.AR || "EitherOrAdvp" in 0.AR || "aAdvpaAdvp" in 0.AR || "EitherOr4Advp" in 0.AR
			  )
		|| 0.UnicodeLemma in {"μηδέ","οὐδέ","μηδέ","οὐδέ","οὐδέ","οὐδέ"} // slight differences in font require all listed
		   && ! ( "PtclCL" in 0.AR )
		   && ! ( "Ptcl2Conj" in 0.BR )
		   && ! ( "Conj2Adv" in 0.AR && "ADV2CL" in 0.AR ) // heb9:25:1
		|| 0.UnicodeLemma in {"μέν","μέν"}
		   && "Conj-CL" in 0.AR
		   && 0.End < tokens.Count-1
		   && ( "οὖν" in tokens[0.End+1,0.End+1].UnicodeLemma || "οὖν" in tokens[0.End+1,0.End+1].UnicodeLemma )
	  }

//Randall 10/28/08 for 2Tm4:10:1
// Converts adv to adj when Adv2Adj applied
[Adv2Adj] adv* -> adj
	? { "Adv2Adj" in 0.AR }

// Converts np to advp	
[np2advp] np* -> advp
	? { "np2advp" in 0.AR 
        || 0.Case == "Dative"
	    && ( "IONp" in 0.AR || "DativeNp" in 0.AR )
      }

/* Commented out because of exact duplicate rule	
[VP2NP] vp* -> np
	? { "VP2NP" in 0.AR }
*/

/* Np post-modified by advp
Applies when NpAdvp applied, unless not boundary applies
*/	
[NpAdvp] np* advp -> np
	? { "NpAdvp" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
      || 1.Case == "Dative"
	  && ( "NpIO" in 0.AR || "NpDative" in 0.AR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; }

// Ptcl functioning as S (make ou)ai/ ptcl2S usually & substantive uses Ptcl2Np & then Np2S)	
[ptcl2S] ptcl* -> S
	? { "ptcl2S" in 0.AR}

// Ptcl functioning as P	
[ptcl2P] ptcl* -> P
	? { "ptcl2P" in 0.AR }

// Randall 6/13/09 mat11:21:4 added to deal with ou)ai really functioning as np in S slot
[intj2Np] intj* -> np
	? { "intj2Np" in 0.AR
		|| 0.UnicodeLemma in { "οὐαί" } 
		   && "Ptcl2Np" in 0.AR // where Logos has intj when old morph had ptcl
	  }


// CONDITIONAL AUTOMATIC PHRASE COMBINATION RULES

/* Periphrastic construction
Applies when "to be" verb in first node and 
participle in second node, unless BeVerb blocked
Or applies when BeVerb rule applied in 0.AR 
Take right boundary and not boundary values from first node
(needed because first node is not head; default always with head)
*/
[BeVerb] verb vp* -> vp
	? { ( ( ( 0.Lemma == "ei)mi/" || 0.UnicodeLemma == "εἰμί" || 0.UnicodeLemma == "εἰμί" )
	      && 1.Mood == "Participle"
	      && ! ( "BeVerb" in 0.BR )
	      )
	    || "BeVerb" in 0.AR
	    )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

/* Periphrastic construction
Applies when "to be" verb in second node and 
participle in first node, unless VerbBe blocked
Or applies when VerbBe rule applied in 0.AR 
*/	
[VerbBe] vp* verb -> vp
	? { ( ( ( 1.Lemma == "ei)mi/" || 1.UnicodeLemma == "εἰμί" || 1.UnicodeLemma == "εἰμί" )
	      && 0.Mood == "Participle"
	      && ! ( "VerbBe" in 0.BR )
	      )
	    || "VerbBe" in 0.AR
	    )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; }

/* O interjection with vocative
Applies automatically when vocative np in second node, 
unless intjNP blocked or not boundary applies
Or applies when both right boundary and intjNP rule manually applied 
Additional manual applications to cases like "woe to you" most likely
need to be manually corrected #
*/
[intjNP] intj np* -> np
	? { ( 1.Case == "Vocative" 
	    && ! ( "intjNP" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
		&& ! ( 0.UnicodeLemma in {"ναί","ναί"} )
	    )
	  || ( (1.End-0.Start) in 0.RB && "intjNP" in 0.AR )
	  } 
	>> { .RB = 0.RB; .NB = 0.NB; }

/* det and np forms np
when case and number in agreement,
and no punctuation in between first and second node,
and gender in agreement or case of det is genitive,
unless the np has det attribute (det promoted to np),
or rule used in second node is NpaNp, Conj3Np, Conj4Np, NP-Demo, Verb2NP, NP-all
or DetAdj applied
or DetNP blocked
or not boundary used
or not both DetNP and right boundary applied (does not go thru with just one)
or DetNP blocked through second node 
also det and np forms np when DetNP applied and right boundary used
*/
[DetNP] det np* -> np
	? { ( 0.Case == 1.Case && 0.Number == 1.Number  // agreement
	    && 0.Punc=="" 
	    && ( 0.Gender == 1.Gender || 0.Case == "Genitive" )
	    && ( !1.HasDet )
	    && ! ( 1.Rule in { "NpaNp", "Conj3Np", "Conj4Np", "NP-Demo","Verb2NP","NP-all","Nump2NP"} )
	    && ! ( "DetAdj" in 0.AR )
	    && ! ("DetNP" in 0.BR )
	    && ( ! ( "DetNP" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "DetNP-1" in 1.BR )
	    )
      || ( 0.Case == 1.Case && 0.Number == 1.Number  // agreement
	    && 0.Punc=="" 
	    && ( 0.Gender == 1.Gender || 0.Case == "Genitive" )
	    && 1.Rule in {"QuanNP","QuanPp" }
	    && ! ( 1.Rule in { "NpaNp", "Conj3Np", "Conj4Np", "NP-Demo","Verb2NP","NP-all","Nump2NP"} )
	    && ! ( "DetAdj" in 0.AR )
	    && ! ("DetNP" in 0.BR )
	    && ( ! ( "DetNP" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "DetNP-1" in 1.BR )
	    )
	  || ( (1.End-0.Start) in 0.RB 
		   && "DetNP" in 0.AR 
		   && ! ( 1.Rule in {"Nump2NP"} )
	  || "DetNP-1" in 1.AR	
		 )
      }
	>> { .HasDet = true; 
	     .RB = 0.RB; .NB = 0.NB;
	   }

/* NP post-modified by a demonstrative
# Matthew used this rule on resumptive use of demo, review (perhaps add RefIndex sufficient)?
# Some cases of Np-Appos even has NP-Demo applied (require demonstrative in 1.Lemma for all)?
Applies when lemma in second node is "e)kei=nos" or "ou(=tos"
and rule used in second note is Pron2NP
and case, gender, number match between first and second node
and no punctuation between first and second node
unless NP-Demo blocked
or not boundary used
or Np2S or Np2P applied in second node
Also an automatic correction set of conditions:
Applies when NpPron had been applied manually (incorrectly)
and the lemma in second node is "that" or "this"
unless not boundary used
Also applies when right boundary used and NP-Demo applied
*/
[NP-Demo] np* np -> np
	? { ( ( 1.Lemma in {"e)kei=nos", "ou(=tos"} || 1.UnicodeLemma in {"ἐκεῖνος", "οὗτος"} )
	    && 1.Rule=="Pron2NP"
	    && 0.Case==1.Case
	    && 0.Gender==1.Gender
	    && 0.Number==1.Number
	    && 0.Punc=="" 
	    && ! ( "NP-Demo" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "np2S" in 1.AR || "Np2S" in 1.AR )
	    && ! ( "np2P" in 1.AR || "Np2P" in 1.AR )
		&& ! ( 0.UnicodeLemma in {"αὐτός","αὐτός"} )
	    )
	  || ( "NpPron" in 0.AR
		 && ( 1.Lemma in {"e)kei=nos", "ou(=tos"} || 1.UnicodeLemma in {"ἐκεῖνος", "οὗτος"} )
		 && ! ( (1.End-0.Start) in 0.NB ) 
		 )
	  || ( (1.End-0.Start) in 0.RB 
		 && "NP-Demo" in 0.AR
		 && ! ( "NP-Demo-1" in 1.BR )
		 )
	  }
	>> { .Punc = 1.Punc; }

/* NP pre-modified by a demonstrative
# Some cases of Np-Appos even has Demo-NP applied (require demonstrative in 0.Lemma for all)?
Applies when lemma in first node is "e)kei=nos" or "ou(=tos"
and rule used is Pron2NP
and case, gender, and number matches
and no punctuation between two nodes,
unless Demo-NP blocked
or no boundary applies
or Np2S or Np2P applied in first node
Also an automatic correction set of conditions:
Applies when PronNP had been applied manually (incorrectly)
and the lemma in first node is "that" or "this"
unless not boundary used
Also applies when both right boundary and Demo-NP applied
*/
[Demo-NP] np np* -> np
	? { ( ( 0.Lemma in {"e)kei=nos", "ou(=tos"} || 0.UnicodeLemma in {"ἐκεῖνος", "οὗτος"} )
	    && 0.Rule=="Pron2NP"
	    && 0.Case==1.Case
   	    && 0.Gender==1.Gender
	    && 0.Number==1.Number
	    && 0.Punc=="" 
	    && ! ( "Demo-NP" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "np2S" in 0.AR || "Np2S" in 0.AR )
   	    && ! ( "np2P" in 0.AR || "Np2P" in 0.AR )
	    )
	  || ( "PronNP" in 0.AR
		 && ( 0.Lemma in {"e)kei=nos", "ou(=tos"} || 0.UnicodeLemma in {"ἐκεῖνος", "οὗτος"} )
		 && ! ( (1.End-0.Start) in 0.NB ) 
		 ) 
	  || ( (1.End-0.Start) in 0.RB && "Demo-NP" in 0.AR )
	  }
	>> { 
		 .RB = 0.RB; .NB = 0.NB;
		 .Punc = 1.Punc;
	   }

/* Det and adjp promoted to np
Note: New condition with token prevents detadj from forming when we want NPofNP because of genitive np after adj;
genitive absolute participles fall in that position in some cases and cause wrong trees that were manually corrected.
Applies when case, gender, and number match, with no punc in between,
unless DetAdj blocked or not boundary applies
or not both DetAdj and right boundary applied (does not work with just one)
or the second node is a substantive
Or applies when both right boundary and DetAdj rule applied
*/
[DetAdj] det adjp* -> np
	? { ( 0.Case == 1.Case 
	    && 0.Gender == 1.Gender
	    && 0.Number == 1.Number
	    && 0.Punc==""
	    && ! ( 1.End < tokens.Count-1 && "Genitive" in tokens[1.End+1,1.End+1].Case ) 
	    && ! ("DetAdj" in 0.BR)
	    && ( ! ( "DetAdj" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && !1.Substantive 
	    )
	  || ( (1.End-0.Start) in 0.RB && "DetAdj" in 0.AR ) 
	  }
	>> { .HasDet = true;
		 .RB = 0.RB; .NB = 0.NB;
	   }

// Needed for Logos morph num where old morph had adj
[DetNump] det nump* -> np
	? { 0.Punc==""
	    && ! ("DetAdj" in 0.BR)
	    && ( ! ( "DetAdj" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && !1.Substantive
		&& 1.functionMorph == "J" 
	  || (1.End-0.Start) in 0.RB 
		 && ( "DetAdj" in 0.AR || "DetNump" in 0.AR ) 
	  }
	>> { .HasDet = true;
		 .RB = 0.RB; .NB = 0.NB;
	   }

/* Participial or Infinitival clauses as np
Applies when mood in first node is participle or infinitive,
unless DetCL (DetVP is the old name still in some AR's) blocked or not boundary applies
or only either DetCL or right boundary applied alone (prevents extra trees)
Or applies when both right boundary and DetCL rule applied (enables right tree)
Right boundary and no boundary values taken from first node 
(needed as first node not head--default is to take values from head node)
*/	
[DetCL] det CL* -> np
	? { ( 1.Mood in {"Participle"} 
	    && ! ( "DetVP" in 0.BR  || "DetCL" in 0.BR )
	    && ( ! ( ( "DetVP" in 0.AR ) || ( "DetCL" in 0.AR ) ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
        && ! ( 1.Rule in {"S-P","S-P-ADV","S-ADV-P"} )
        )
        || ( 1.Mood in {"Infinitive"} 
	    && ! ( "DetVP" in 0.BR  || "DetCL" in 0.BR )
	    && ( ! ( ( "DetVP" in 0.AR ) || ( "DetCL" in 0.AR ) ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
        && ! ( "CL2S" in 1.AR )
	    )
	  || ( (1.End-0.Start) in 0.RB && ( "DetVP" in 0.AR || "DetCL" in 0.AR) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

/* Det and adv promoted to advp,
unless DetAdv blocked or not boundary applies */	
[DetAdv] det adv* -> advp
	? { ! ( "DetAdv" in 0.BR ) 
	  && ! ( (1.End-0.Start) in 0.NB )
	  && ! ( "Det2NP" in 0.AR ) // prevents problem from mei=zon shifting to adv in mat20:31
	  }
	>> { .RB = 0.RB; .NB = 0.NB; } 

/* Np premodified by adjp
Applies when case, gender, and number ({"mhdei/s" not need match) match	
unless second node is a relative
or AdjpNp (AdjNP was old name still in some AR's) blocked in first or second node
or the lemma in first node is "pa=s" ("all") or "o(/los" ("whole" (All-NP applies instead)
or second node uses rules Det2NP or Np-Appos
or only either AdjNP or right boundary applied alone (prevents extra trees)
or AdjpAdjp applied and right boundary used
or not boundary applies
Also applies when both right boundary and AdjpNp applied (enables right tree)
(ruling out "all" & "whole" ensures that manually entered wrong trees do not work)
*/  
[AdjpNp] adjp np* -> np
	? { ( 0.Case == 1.Case
	    && 0.Gender == 1.Gender
	    && ( 0.Number == 1.Number || ( 0.Lemma in {"mhdei/s"} || 0.UnicodeLemma in {"μηδείς"} ) )
	    && ! ( 1.Relative || 1.Type=="Relative" )
	    && ! ("AdjNP" in 0.BR || "AdjpNP" in 0.BR || "AdjpNp" in 0.BR )
	    && ! ( "AdjNP-1" in 1.BR || "AdjpNP-1" in 1.BR || "AdjpNp-1" in 1.BR )
   	    && ! ( 0.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"} ) // different font for JT & SBL morph requires apparent redundancy
	    && ! ( 1.Rule in {"Det2NP","Np-Appos"} )
	    && ( ! ( ( "AdjNP" in 0.AR ) || ( "AdjpNP" in 0.AR ) || ( "AdjpNp" in 0.AR) ) || (1.End-0.Start) in 0.RB )
	    && ! ( "AdjpAdjp" in 0.AR && (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || 0.Case == 1.Case
	     && 0.Number == 1.Number
		 && 0.UnicodeLemma in {"σός"}
	  || ( (1.End-0.Start) in 0.RB 
		   && ( "AdjNP" in 0.AR || "AdjpNP" in 0.AR || "AdjpNp" in 0.AR )
		   && ! ( 0.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"} )
		   && ! ( "AdjNP-1" in 1.BR || "AdjpNP-1" in 1.BR || "AdjpNp-1" in 1.BR )
		 )
	  } 
	>> { 
	     .RB = 0.RB; .NB = 0.NB;
	   }

// Added to accommodate new num category from Logos morph
[NumpNP] nump np* -> np
	? { ! ( "NumpNP" in 0.BR )
		&& ! ( 1.Rule in {"DetCL"} )
		&& ! ( (1.End-0.Start) in 0.NB )
		&& ! ( 1.UnicodeLemma in {"περισσεύω","αὐτός"} )
		|| "NumpNP" in 0.AR
		   && ( 1.End-0.Start) in 0.RB
		   && ! ("NumpNP-1" in 1.BR )
		|| "AdjNP" in 0.AR
		   && ( 1.End-0.Start) in 0.RB	
	  } 
	>> { 
	     .RB = 0.RB; .NB = 0.NB;
	   }
	   
 // Adjp modified by genitive np
 /* Note: This rule is now restricted to 0.Lemma in {"me/sos","mesto/s","a)/xios", "e)/nocos",
 "plh/rhs", "me/gas", "prw=tos", "ta/cion"}, while all remainding cases get shifted into NPofNP.
Applies when np in second node = genitive case
and no punctuation between first and second node
and the word in first node not a substantive
and AdjpofNP not blocked in first or second node
and np in second node not = dative case
and the adjp in first node not product of DetAdj rule (which is interpreted as an np)
and AdjpNp (AdjNP is the old name still left in some trees that has to be kept to keep
old trees from falling apart) not applied in first node
and not have only AdjpofNP or only the right boundary (both need to be present)
and not boundary does not apply
and the lemma in first node is not "all" with its case being Genitive
Or both AdjpofNP and right boundary both manually applied
unless the adjp in first node is a substantive (to curb wrong manual applications)
and not AdjpofNP-1 applied in second node 
Or AdjpofNP-1 in 1.AR with right boundary (this was added to bypass cases where
the morph says the adjective is a substantive and yet it is wrong)
 */
[AdjpofNp] adjp* np -> adjp
	? { ( 1.Case == "Genitive"
	    && 0.Punc=="" 
	    && !0.Substantive
	    && ( 0.Lemma in {"me/sos","mesto/s","a)/xios","e)/nocos","plh/rhs","me/gas","prw=tos","ta/cion","cei/rwn","a)kata/paustos","a)/peiros"}
            || 0.UnicodeLemma in {"μέσος","μεστός","ἄξιος","ἔνοχος","πλήρης","μέγας","πρῶτος","ταχέως","χείρων","ἀκατάπαυστος","ἄπειρος"} )
//	    && ! ( 0.Lemma in { "ei(=s", "e(/teros", "a)/llos" } ||  0.UnicodeLemma in { "εἷς", "ἕτερος", "ἄλλος" } )
	    && ! ( "AdjpOfNP" in 0.BR || "AdjpofNp" in 0.BR )
	    && ! ( "AdjpOfNP-1" in 1.BR || "AdjpofNp-1" in 1.BR )
	    && 1.Case != "Dative"
	    && ! ( 0.Rule in { "DetAdj" } )
	    && ! ( "AdjNP" in 0.AR || "AdjpNP" in 0.AR || "AdjpNp" in 0.AR )
/* 		&& ! ( 0.Start > 1 
 			 && "det" in tokens[0.Start-1,0.Start-1].Cat
			 && 0.Case in tokens[0.Start-1,0.Start-1].Case 
			 && 0.Gender in tokens[0.Start-1,0.Start-1].Gender 
			 && 0.Number in tokens[0.Start-1,0.Start-1].Number
			 ) */
	    && ( ! ( "AdjpOfNP" in 0.AR || "AdjpofNp" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( 0.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"} // addresses both JT & SBL font differences
	           && 0.Case == "Genitive"
	         )
	    )
	  || ( ( "AdjpOfNP" in 0.AR || "AdjpofNp" in 0.AR )
		   && (1.End-0.Start) in 0.RB
		   && !0.Substantive 
		   && ! ( "AdjpOfNP-1" in 1.BR || "AdjpofNp-1" in 1.BR )
		 )
	  || ( ( "AdjpOfNP-1" in 1.AR || "AdjpofNp-1" in 1.AR )
	     && (1.End-0.Start) in 0.RB 
		 )
	  }
	>> { .Punc = 1.Punc; }


// Np modified by genitive np
/* 
Modified this rule to ensure that AdjpofNP is mutually exclusive with NPofNP
Applies when np in second node = Genitive case
and np in first node not product of one of the listed rules
and np in second node not product of one of the listed rules
and lemma in first node not "midst," "full," "worthy"
and NPofNP not blocked in first or second node
and not ofNPNP applied in first node
and not just NPofNP or just right boundary applied 
(both needed to apply manually; automatic application does not require either)
and not P-S applied in first node with right boundary
and not AdjpAdjp applied in first node with right boundary
and not boundary not applied
and the lemma in first node is not "all" with its case being Genitive
and not have first and second node lemmas in Lord Jesus, Jesus Christ, and Christ Jesus
Or applies automatically when right boundary applied (and we have np np)
and case of second node not nominative, dative, or accusative
and rule in first node not one from the list
and rule in second node not one from the list
and rule in first node not Pron2NP and case is genitive
and NPofNP not blocked in first node (NPofNP) or second node (with NPofNP-1)
and ofNPNP not applied in 0.AR
and neither P-S nor AdjpAdjp applied 0.AR with right boundary
and not boundary not applied
Or applies manually when right boundary and NPofNP applied; unless blocked in second node
and not have first and second node lemmas in Lord Jesus, Jesus Christ, and Christ Jesus
Or applies when first node is substantive
even though AdjpofNP was applied in first node
as long as right boundary also present
and NPofNP not blocked (this alternate condition is to correct wrong manual application)
*/	  
[NPofNP] np* np -> np
	? { ( 1.Case == "Genitive" 
	    && ! ( 0.Rule in {"DetNP", "DetAdj", "NpPp", "QuanPp", "NpaNp", "Conj3Np", "Conj4Np", "Np-Appos", "AdvpNp", "NPofNP", "QuanNP", "Verb2NP"} )
	    && ! ( 0.Rule in {"Pron2NP","AdjpNp","All-NP","DetCL","Det2NP", "NumpNP"} )
        && ! ( 1.Rule in {"CL2NP","Det2NP","CL2NP"} )
		&& ! ( 0.Rule in {"CL2NP"} && 0.Mood!="Participle" )
	    && ! ( 0.Lemma in {"me/sos","mesto/s","a)/xios"} || 0.UnicodeLemma in {"μέσος","μεστός","ἄξιος"} )
	    && ! ( "NPofNP" in 0.BR )
	    && ! ("NPofNP-1" in 1.BR)
	    && ( ! ("NPofNP" in 0.AR) || (1.End-0.Start) in 0.RB )
	    && ! ( "ofNPNP" in 0.AR )
		&& ! ( "Demo-NP" in 0.AR )
		&& ! ( "ofNPNP-1" in 1.AR )
	    && ! ( "P-S" in 0.AR && (1.End-0.Start) in 0.RB )
	    && ! ( "AdjpAdjp" in 0.AR && (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( 0.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"}
	           && 0.Case == "Genitive"
	         )  
	  	&&! ( 0.Lemma=="ku/rios" && 1.Lemma=="i)hsou=s" ) // "Lord Jesus"
	    &&! ( 0.Lemma=="cristo/s" && 1.Lemma=="i)hsou=s" ) // "Christ Jesus"
	    &&! ( 0.Lemma=="i)hsou=s" && 1.Lemma=="cristo/s" ) // "Jesus Christ"
	  	&&! ( 0.UnicodeLemma in {"κύριος","κύριος"} && 1.UnicodeLemma=="Ἰησοῦς" ) // "Lord Jesus"
	    &&! ( 0.UnicodeLemma in {"Χριστός","Χριστός"} && 1.UnicodeLemma=="Ἰησοῦς" ) // "Christ Jesus"
	    &&! ( 0.UnicodeLemma=="Ἰησοῦς" && 1.UnicodeLemma in {"Χριστός","Χριστός"} ) // "Jesus Christ"       
		&& ! ( 0.Rule in {"CL2NP"} && 0.ClType=="Verbal" &&! ( 0.Mood in {"Participle"} ) )       
//	    && 0.Punc==""
	    )
	  || ( (1.End-0.Start) in 0.RB
		 && ! ( 1.Case in { "Nominative", "Dative", "Accusative", "Vocative" } )
	     && ! ( 0.Rule in {"Det2NP","DetAdj","DetCL","NpPp","QuanPp"} )
	     && ! ( 1.Rule in {"CL2NP","Det2NP"} )
	     && ! ( 0.Rule == "Pron2NP" && 0.Case == "Genitive" )
	     && ! ("NPofNP" in 0.BR)
	     && ! ("NPofNP-1" in 1.BR)
	     && ! ( "ofNPNP" in 0.AR )
		 && ! ( "Demo-NP" in 0.AR )
		 && ! ( "ofNPNP-1" in 1.AR )
	     && ! ( "P-S" in 0.AR && (1.End-0.Start) in 0.RB )
	     && ! ( "AdjpAdjp" in 0.AR && (1.End-0.Start) in 0.RB )
	     && ! ( (1.End-0.Start) in 0.NB )
		&& ! ( 0.Rule in {"CL2NP"} && 0.ClType=="Verbal" &&! ( 0.Mood in {"Participle"} ) )
//	     && 0.Punc==""
	     )
	  || ( (1.End-0.Start) in 0.RB && "NPofNP" in 0.AR && ! ("NPofNP-1" in 1.BR) 
	  	 &&! ( 0.Lemma=="ku/rios" && 1.Lemma=="i)hsou=s" ) // "Lord Jesus"
	     &&! ( 0.Lemma=="cristo/s" && 1.Lemma=="i)hsou=s" ) // "Christ Jesus"
	     &&! ( 0.Lemma=="i)hsou=s" && 1.Lemma=="cristo/s" ) // "Jesus Christ"
	  	 &&! ( 0.UnicodeLemma in {"κύριος","κύριος"} && 1.UnicodeLemma=="Ἰησοῦς" ) // "Lord Jesus"
	     &&! ( 0.UnicodeLemma in {"Χριστός","Χριστός"} && 1.UnicodeLemma=="Ἰησοῦς" ) // "Christ Jesus"; "Χριστός" and "Χριστός" apparently are different in font in some way
	     &&! ( 0.UnicodeLemma=="Ἰησοῦς" && 1.UnicodeLemma in {"Χριστός","Χριστός"} ) // "Jesus Christ"
		 )
	  || ( 0.Substantive
		   && ( "AdjpOfNP" in 0.AR || "AdjpofNp" in 0.AR ) 
		   && (1.End-0.Start) in 0.RB 
		   && ! ( "NPofNP" in 0.BR )
		 )
	  || ( 1.Case == "Genitive"
	       && "NpPron" in 0.AR
		   && (1.End-0.Start) in 0.RB
		   && ! ( 0.Rule in {"DetNP", "NpPp", "QuanPp", "NpaNp", "Conj3Np", "Conj4Np", "Np-Appos", "AdvpNp", "NPofNP", "QuanNP", "Verb2NP"} )
	       && ! ( 0.Rule in {"Pron2NP","AdjpNp","All-NP","DetCL","Det2NP","NumpNP"} )
	       && ! ( 1.Rule in {"CL2NP","Det2NP"} )
	       && ! ("NPofNP-1" in 1.BR)
	       && ! ( "ofNPNP" in 0.AR )
	       && ! ( (1.End-0.Start) in 0.NB )
	     ) 
	  }
	>> { // .HasDet = false; 
		 .Punc = 1.Punc;
	   }

/* Pron premodifying np	#hO TI pronCL, relative clause status obscured by PronNP rule (relative not head)
Inconsistently applied;"οὗτος" should use Demo-NP instead
Kept only for intensive uses of au)to/s and "ὅς" postmodified by "antecedent"
All other cases shifted to Pron2NP and ofNPNP as appropriate
Case, gender, and number must match, with no punctuation in between nodes
and np in second node not product of CL2NP rule
and PronNP not blocked
and not boundary not applied
Or UnicodeLemma in first node is "himself, herself, itself"
and UnicodeLemma in second node is "I" or "you"
and case and number match, with no punctuation in between nodes
and np in second node not product of CL2NP rule
and PronNP not blocked
and not boundary not applied 
*/
[PronNP] pron np* -> np
	? { ( ( 0.Lemma in {"au)to/s", "o(/s"} || 0.UnicodeLemma in {"αὐτός","αὐτός", "ὅς"} )
		&& 0.Case==1.Case
	    && 0.Gender == 1.Gender
	    && 0.Number == 1.Number
	    && 0.Punc==""
	    && ! ( 1.Rule in {"CL2NP"} )
	    && ! ( "PronNP" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
	     )
	     || ( ( 0.Lemma in {"au)to/s"} || 0.UnicodeLemma in {"αὐτός","αὐτός"} )
			&& ( 1.Lemma in {"e)gw/", "su/"} || 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ"} )
		    && 0.Case==1.Case
			&& 0.Number == 1.Number
	        && 0.Punc==""
	        && ! ( 1.Rule in {"CL2NP"} )
	        && ! ( "PronNP" in 0.BR )
	        && ! ( (1.End-0.Start) in 0.NB )
	        )
	     || "PronNP-1" in 1.AR
			&& ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

/* Pron postmodifying np 
Inconsistently applied;"οὗτος" should use NP-Demo instead
Kept only for intensive uses of au)to/s
All other cases shifted to Pron2NP and then NP-Appos and NPofNP as appropriate
Added the novel "NpPron-1" in 1.AR to address 0.Case && 1.Case==Genitive and yet NpPron
*/
[NpPron] np* pron -> np
	? { ( ( 1.Lemma=="au)to/s" || 1.UnicodeLemma in {"αὐτός","αὐτός"} )
		&& 0.Case==1.Case
	    && 0.Gender == 1.Gender
	    && 0.Number == 1.Number
		&& 0.Punc==""
		&& "NpPron" in 0.AR
		&& ! ( 0.Case=="Genitive" && 1.Case=="Genitive" )
	    && ! ( 0.Rule in {"CL2NP"} )
	    && ! ( "NpPron" in 0.BR )
		&& ! ( (1.End-0.Start) in 0.NB )
		)
	  || ( ( 1.Lemma in {"au)to/s"} || 1.UnicodeLemma in {"αὐτός","αὐτός"} )
		&& ( 0.Lemma in {"e)gw/", "su/"} || 0.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ"} )
		&& 0.Case==1.Case
		&& 0.Number == 1.Number
	    && 0.Punc==""
		&& ! ( 0.Case=="Genitive" && 1.Case=="Genitive" )
	    && ! ( 0.Rule in {"CL2NP"} )
	    && ! ( "NpPron" in 0.BR )
		&& ! ( (1.End-0.Start) in 0.NB )
	     )
	  || "NpPron-1" in 1.AR
	  }
	>> { .Punc = 1.Punc; }

// Appositive
/*
Apposition now covers identifying and attributive relationships;
the difference from NpAdjp lies in np to np relationship (even when
the second np was originally an adj, which is turned into an np by det
Applies when case, gender, and number match
and np in second node product of DetNP rule
and np in first node not product of listed rules
and np in second node not product of listed rules
and no determiner in front of np in first node
and Np-Appos not blocked in first or second node
and not just Np-Appos or just right boundary applied
(both needed for manual application; the presence of just one blocks automatic application)
and not boundary not applied
and np in first node not product of Np-Appos or NPofNP rule while np  
in second node not product of DetNP rule
Or manual application with right boundary and Np-Appos applied in first node
unless rule in second node=DetAdj or Np-Appos blocked in second node
Or automatically applies in the cases of Lord Jesus, Christ Jesus, and Jesus Christ, 
unless not boundary is applied
Or manual entry of NP-Adjunct and right boundary shifted over to Np-Appos
Or failsafe "NP-Appos-1" applied in second node with right boundary
*/
[Np-Appos] np* np -> np
	? { ( 0.Case == 1.Case
		&& 0.Gender == 1.Gender
	    && 0.Number == 1.Number
	    && 1.Rule in {"DetNP"}
	    && ! ( 0.Rule in {"Pron2NP","DetAdj","Adj2NP","Verb2NP","CL2NP"} )
	    && ! ( 1.Rule in {"NP-Demo"} )
	    && ! ( 0.Start > 1 && "det" in tokens[0.Start-1,0.Start-1].Cat)
	    && ! ( "NP-Appos" in 0.BR || "Np-Appos" in 0.BR )
	    && ! ( "NP-Appos-1" in 1.BR || "Np-Appos-1" in 1.BR )
	    && ( ! ( "NP-Appos" in 0.AR || "Np-Appos" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( 0.Rule in {"Np-Appos","NPofNP", "QuanNP"} && 1.Rule in {"DetNP"} )
		&& ! ( "NPDetAdj" in 0.AR && 1.UnicodeLemma in {"ἐμός","σός","ὑμέτερος","ἡμέτερος","ἐμός","σός","ὑμέτερος","ἡμέτερος"} )
	    )
	  || ( (1.End-0.Start) in 0.RB 
		   && ( "NP-Appos" in 0.AR || "Np-Appos" in 0.AR )
		   && ! ( 1.Rule in {"DetAdj"} )
		   && ! ( "NP-Appos-1" in 1.BR || "Np-Appos-1" in 1.BR )  
	//	   && ! ( "np2CL" in 1.AR || "Np2CL" in 1.AR || 0.Case=="Vocative" )// to get vocatives as separate clauses, though similar to apposition
		   && ! ( "NPDetAdj" in 0.AR && 1.UnicodeLemma in {"ἐμός","σός","ὑμέτερος","ἡμέτερος","ἐμός","σός","ὑμέτερος","ἡμέτερος"} )
		 )
	  || ( ! ( (1.End-0.Start) in 0.NB ) && 0.Lemma=="ku/rios" && 1.Lemma=="i)hsou=s" ) // "Lord Jesus"
	  || ( ! ( (1.End-0.Start) in 0.NB ) && 0.Lemma=="cristo/s" && 1.Lemma=="i)hsou=s" ) // "Christ Jesus"
		 && ! ( "NP-Appos" in 0.BR || "Np-Appos" in 0.BR )
	  || ( ! ( (1.End-0.Start) in 0.NB ) && 0.Lemma=="i)hsou=s" && 1.Lemma=="cristo/s" ) // "Jesus Christ"
	  || ( ! ( (1.End-0.Start) in 0.NB ) && 0.UnicodeLemma in {"κύριος","κύριος"} && 1.UnicodeLemma=="Ἰησοῦς" ) // "Lord Jesus"; both "κύριος","κύριος" needed because of font differences
	  || ( ! ( (1.End-0.Start) in 0.NB ) && 0.UnicodeLemma in {"Χριστός","Χριστός"} && 1.UnicodeLemma=="Ἰησοῦς" ) // "Christ Jesus"
		 && ! ( "NP-Appos" in 0.BR || "Np-Appos" in 0.BR )
	  || ( ! ( (1.End-0.Start) in 0.NB ) && 0.UnicodeLemma=="Ἰησοῦς" && 1.UnicodeLemma in {"Χριστός","Χριστός"} ) // "Jesus Christ"
	  || "NP-Adjunct" in 0.AR
		 && (1.End-0.Start) in 0.RB
	  || ( "NP-Appos-1" in 1.AR || "Np-Appos-1" in 1.AR )
	     && (1.End-0.Start) in 0.RB
/*	  || ( "NP-AdjP" in 0.AR || "NpAdjp" in 0.AR )
	     && 1.Rule in {"AdjpofNp"}
	     && (1.End-0.Start) in 0.RB
	     && 0.Case == 1.Case
		 && 0.Gender == 1.Gender
	     && 0.Number == 1.Number
	     && ! ( "NP-Appos" in 0.BR || "Np-Appos" in 0.BR ) */
	  } 
	>> { .Punc = 1.Punc; }
  
// Np post-modified by Det-Adj 
/*
While I at first modified this rule to make sure it does not apply when
head np has a det, I changed my mind
Applies automatically when np in second node product of DetAdj rule
and case, gender, and number match, with no punctuation between nodes
and case of first node not nominative (to bypass extra trees in S-P, P-S 
situations, meaning that cases with nominative have to be manually entered)
and np in first node not product of DetNP rule
and NPDetAdj not blocked
and not boundary not applied
Or manual application with right boundary and NPDetAdj applied in first node
unless np in first node product of DetNP rule 
*/
[NPDetAdj] np* np -> np
	? { ( 1.Rule == "DetAdj"
	    && 0.Case == 1.Case
	    && 0.Gender == 1.Gender
	    && 0.Number == 1.Number
	    && 0.Punc=="" 
	    && 0.Case != "Nominative"
	    && ! ( 0.Rule in {"DetNP","NumpNP"} )
	    && ! ("NPDetAdj" in 0.BR)
	    && ! ( (1.End-0.Start) in 0.NB )
		&& ! ( "NP-Appos-1" in 1.AR || "Np-Appos-1" in 1.AR )
	    )
	   || ( 1.Rule in {"DetAdj"}
	   	  && 0.Case == 1.Case
		  && 0.Gender == 1.Gender
	      && 0.Number == 1.Number
		  && (1.End-0.Start) in 0.RB 
		  && ( "NP-Appos" in 0.AR || "Np-Appos" in 0.AR )
	      && ! ( 0.Rule in {"DetNP", "DetAdj","NumpNP"} )
	      && ! ("NPDetAdj" in 0.BR)
	      && ! ( (1.End-0.Start) in 0.NB )
		  )   
	   || ( (1.End-0.Start) in 0.RB && "NPDetAdj" in 0.AR 
	   	  && ! ( 0.Rule in {"DetNP","NumpNP"} )
		  && ! ( 1.Rule in {"CL2NP"} )
/*	   	  && ! ( 0.Start > 1 
 			   && "det" in tokens[0.Start-1,0.Start-1].Cat
			   && 0.Case in tokens[0.Start-1,0.Start-1].Case 
			   && 0.Gender in tokens[0.Start-1,0.Start-1].Gender 
			   && 0.Number in tokens[0.Start-1,0.Start-1].Number
			   ) */
		  )
	   }
	 >> { .Punc = 1.Punc; }
	   
/* Np post-modified by adjp
Applies automatically whe case, gender, and number match
with no punctuation between nodes
and np in first node not product of listed rules
and NP-Adjp not blocked
and UnicodeLemma in second node not "all" or "whole"
and not AdjpAdjp and right boundary applied together
and not boundary not applied
Or manual application with right boundary and NP-Adjp
unless UnicodeLemma in second node is "all" or "whole" (correct wrong manual application)
*/
[NpAdjp] np* adjp -> np
	? { ( 0.Case == 1.Case
	    && 0.Gender == 1.Gender
	    && 0.Number == 1.Number
	    && 0.Punc=="" 
	    && ! ( 0.Rule in {"Det2NP","NpPp","QuanPp"} )
//	    && ! ( 0.Rule in {"NPofNP", "QuanNP", "Np-Appos","Det2NP","DetNP","Verb2NP"} )
	    && ! ( "NP-AdjP" in 0.BR || "NpAdjp" in 0.BR )
	    && ! ( 1.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"} )
	    && ! ( "AdjpAdjp" in 0.AR && (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( (1.End-0.Start) in 0.RB 
		   && ( "NP-AdjP" in 0.AR || "NpAdjp" in 0.AR ) 
   	       && ! ( 1.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"} )
		   )
	  }
	>> { .Punc = 1.Punc; }

// Added to accommodate new category num in Logos morph
[NpNump] np* nump -> np
	? { ! ( "NPNump" in 0.BR || "NpNump" in 0.BR )
		&& ! ( "AdjNP" in 1.AR || "adjp2ADV" in 1.AR || "Adj2NP" in 1.AR || "Nump2NP" in 1.AR )
		&& ! ( 0.Rule in {"Nump2NP","DetNump"} )
		&& ! ( (1.End-0.Start) in 0.NB )
		&& ! ( "Adj2NP" in 1.BR )
		&& ! ( 0.UnicodeLemma in {"περισσεύω","αὐτός","αὐτός"} )
	//	&& (0.End-0.Start) in 1.RB
		|| ( "NPNump" in 0.AR || "NpNump" in 0.AR )
		   && (1.End-0.Start) in 0.RB
		   && ! ( "NPNump-1" in 1.BR || "NpNump-1" in 1.BR )
	  }
	  
// Np post-modified by pp 
[NpPp] np* pp -> np
	? { ( ! ( 0.Rule in {"AdvpNp", "DetNP", "AdjpNp", "All-NP", "NumpNP", "NpaNp","Pron2NP","Verb2NP"} ) 
	    && ! ( 0.Rule in {"NpPp", "QuanPp", "Np-Appos","Vp2Np","CL2NP","DetCL"} )
	    && 1.Case != "Accusative"
	    && 0.Punc=="" 
	    && ! ( 1.Relative || 1.Type=="Relative" )
	    && ! ( "NP-PP" in 0.BR || "NpPp" in 0.BR )
	    && ! ( 1.PrepLemma in {"ἐκ","e)pi/","dia/","u(po/","e)n","ei)s","a)po/"} ) // old lemma (kept in case needed for backward compatibility in the future)
        && ! ( 1.PrepLemma in {"ἐκ","ἐπί","διά","ὑπό","ἐν","εἰς","ἀπό"} ) // JT morph (different from SBL morph & so next line is different in addressing the SBL morph
        && ! ( 1.PrepLemma in {"ἐκ","ἐπί","διά","ὑπό","ἐν","εἰς","ἀπό"/*,"περί"*/} ) // "by" "at" "through" "by" "among" "into" "from"
	    && ! ( 0.Rule == "NpAdjp" && 0.Children[1].Rule == "vp2ADJ" )
	    && ( ! ( "NP-PP" in 0.AR || "NpPp" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( (1.End-0.Start) in 0.RB && ( "NP-PP" in 0.AR || "NpPp" in 0.AR ) &&! ( "NP-PP-1" in 1.BR || "NpPp-1" in 1.BR ) 
         )
     }
	>> { .HasPostPP = true; 
		 .Punc = 1.Punc;
	   }

// Relative clause
/* Reason for removal: Pronoun now interpreted as having a function inside relative clause)
[pronCL] pron CL* -> CL
	? { ( ( 0.Relative || 0.Type=="Relative") 
	    && ! ( "pronCL" in 0.BR )
	    && ( ! ( "pronCL" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( "pronCL" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) )
	  }
	>> { .Type="Relative"; 
	     if ( 1.Case=="" ) .Case = 0.Case;
	     .RB = 0.RB; .NB = 0.NB;
	   }
*/

/* Np postmodified by CL
Meant to apply relative clauses and infinitival clauses that modify np
*/
[NP-CL] np* CL -> np
	? { ( ( ( 1.Relative || 1.Type=="Relative" ) || 1.Mood in {"Infinitive"} ) 
	    && (1.End-0.Start) in 0.RB
	    && ! ( "NP-CL" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "NP-CL-1" in 1.BR ) 
        && ! ( 0.Mood in {"Infinitive"} && "CL2S" in 0.AR )
	    )
	  || ( (1.End-0.Start) in 0.RB && "NP-CL" in 0.AR && ! ( "NP-CL-1" in 1.BR ) )
	  } 
	>> { .Punc = 1.Punc; }

// Np premodified by advp	
[AdvpNp] advp np* -> np
	? { ( ( 0.Lemma in {"kai/", "ou)" } || 0.UnicodeLemma in {"καί", "οὐ", "καί" } || 0.encode in {"οὐ"} )
	    && 0.Punc=="" 
	    && ! ( "AdvNP" in 0.BR || "AdvpNp" in 0.BR )
	    && ( ! ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
        && ! ( 1.Mood in {"Infinitive"} && "CL2S" in 1.AR )
	    )
	  || ( (1.End-0.Start) in 0.RB && ( "AdvNP" in 0.AR || "AdvpNp" in 0.AR ) )
	  || 0.UnicodeLemma in { "καί" } // to deal with Logos morph having adverbial KAI as ptcl
		 && 0.functionMorph == "BE"
		 && ( "advp2ADV " in 0.BR || "Advp2ADV" in 0.BR )
	  || ( "AdvADJP" in 0.AR || "AdvpAdjp" in 0.AR )
		 && ! ( (1.End-0.Start) in 0.NB )
		 && ! ( "AdvNP" in 0.BR || "AdvpNp" in 0.BR )
		 && ! ( 1.Substantive || 1.Rule=="Adj2NP" )
	  || ( "AdvNP-1" in 1.AR || "AdvpNp-1" in 1.AR ) // luk22:59:1
		 && (1.End-0.Start) in 0.RB
      || 0.Case == "Dative"
	     && ( "IONp" in 0.AR || "DativeNp" in 0.AR )
	     && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Np postmodifed by adjp "all" & "whole"
[NP-all] np* adjp -> np
	? { ( 1.UnicodeLemma in {"πᾶς","ὅλος","πᾶς", "ὅλος"} // JT & SBL
	    && 0.Case == 1.Case
	    && 0.Number == 1.Number
	    && 0.Punc=="" 
	    && ! ( "NP-all" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
		&& ! ( 0.Rule in {"Demo-NP"} )
	    )
	  || ( (1.End-0.Start) in 0.RB && "NP-all" in 0.AR )
  	  || ( (1.End-0.Start) in 0.RB 
		   && ( "NP-AdjP" in 0.AR || "NpAdjp" in 0.AR ) 
   	       && 1.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"}
		   && ! ( 0.Rule in {"Demo-NP"} )
		   )
	  }
	>> { .Punc = 1.Punc; }

// Np premodifed by adjp "all" & "whole"
[All-NP] adjp np* -> np
	? { ( 0.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"} // JT & SBL
	    && 0.Case == 1.Case
	    && 0.Number == 1.Number
	    && 0.Punc=="" 
	    && ! ( "All-NP" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
		&& ! ( 1.Rule in {"NP-Demo"} )
	    )
	  || ( (1.End-0.Start) in 0.RB && "All-NP" in 0.AR )
	  || ( (1.End-0.Start) in 0.RB 
		   && ( "AdjNP" in 0.AR || "AdjpNP" in 0.AR || "AdjpNp" in 0.AR )
		   && 0.UnicodeLemma in {"πᾶς","ὅλος","πᾶς","ὅλος"}
		   && ! ( 1.Rule in {"NP-Demo"} )
		 )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 1.Punc; 
	   }

// Np premodified by prep	  
[PrepNp] prep np* -> pp
	? { ! ( "PrepNP" in 0.BR || "PrepNp" in 0.BR )  
	  && ! ( (1.End-0.Start) in 0.NB )
	//  && ! ( 1.Lemma in {"me/sos","dexio/s","eu)w/numos"} )
	//  && ! ( 1.UnicodeLemma in {"μέσος","δεξιός","εὐώνυμος"} )
      && ! ( 1.Mood in {"Infinitive"} && "CL2S" in 1.AR )
	  || ( 1.Rule in {"DetAdj"}
		 && ( 1.Lemma in {"me/sos"} || 1.UnicodeLemma in {"μέσος"} ) // ,"δεξιός","εὐώνυμος"}   
	  	 && ! ( "PrepNP" in 0.BR || "PrepNp" in 0.BR )
		 )
	  || ( (1.End-0.Start) in 0.RB 
		 && ( "PrepNP" in 0.AR || "PrepNp" in 0.AR )
		 )
	  }
	>> { if ( 0.Lemma == "u(po/" || 0.UnicodeLemma == "ὑπό" ) .ByPhrase = true; 
	     .PrepLemma = 0.UnicodeLemma;
	     .RB = 0.RB; .NB = 0.NB;
	     .Case = "";
	   }

/*
Previously used only in 4 places: Mrk6:2:1 (2 trees); 2Tm2:14:1. Used V-O (from CL2O)
[VP-CL] vp* CL -> vp
	? { ( ( 0.Lemma in {"o)fei/lw","me/llw"} || 0.UnicodeLemma in {"ὀφείλω","μέλλω"} ) // "should", "will"
	    && 1.Mood=="Infinitive"
	    && ! ( "VP-CL" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( (1.End-0.Start) in 0.RB && "VP-CL" in 0.AR )  
	  }
	>> { .Punc = 1.Punc; }
*/


// MANUALLY APPLIED PHRASE COMBINATION RULES
/* These rules need to be reviewed for whether automatic application, 
commplete or partial, is possible
*/

// only used twice in Gal3:16:11 where partial quotation of OT leaves conj with np
[ConjNp] conj np* -> np
	? { "ConjNp" in 0.AR }

/* Previously used for pri/n h)/ ("before") and the resultant conj turned to pp using Conj2Prep
[advconj] adv* conj -> conj
	? { "advconj" in 0.AR 
	  && (1.End-0.Start) in 0.RB
	  }
*/

/* EI MH and EAN MH all switched to MH as separate ADV
[ConjAdv2Prep] conj* adv -> prep
	? { "ConjAdv2Prep" in 0.AR 
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
*/

/*
[ConjAdv2Adv] conj* adv -> adv
	? { "ConjAdv2Adv" in 0.AR 
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
*/

// Randall 7/30/11 added back for luk12:51
[ConjConj] conj* conj -> conj
	? { "ConjConjx" in 0.AR }
	>> { .Punc = 1.Punc; }

// Adjp modified by dative np
/*
Product not automatically promoted to np as dative np modification is not always 
itself specific enough to nominalize
So, it can only be manually applied when the second node is dative in case
and AdjpIO or AdjpComp or AdjpDative had been applied
unless not boundary applies
*/
[AdjpDative] adjp* np -> adjp
      ? { 1.Case == "Dative"
        && ( "AdjpIO" in 0.AR || "AdjpComp" in 0.AR || "AdjpDative" in 0.AR )
        && ! ( (1.End-0.Start) in 0.NB )
        }
      >> { .Punc = 1.Punc; }

// Adjp postmodified by CL
/*
Product not automatically promoted to np as CL modification not always 
itself specific enough to nominalize; by adopting this policy, decision made 
that det modification (interpreted as always functioning as a nominalizer) 
is the only reliable indication of nominalization
*/
[AdjpCL] adjp* CL -> adjp
	? { (1.End-0.Start) in 0.RB
	  && "AdjpCL" in 0.AR
	  }
	>> { .Punc = 1.Punc; }

// Adjp postmodified by pp
/*
Product not automatically promoted to np as pp modification is not always 
itself specific enough to nominalize
*/
[AdjpPp] adjp* pp -> adjp
	? { (1.End-0.Start) in 0.RB
	  && "AdjpPp" in 0.AR
	  }
	>> { .Punc = 1.Punc; }

//Randall 9/1/08 for act26:30:1
[ofNPAdjp] np adjp* -> adjp
	? { (1.End-0.Start) in 0.RB 
	  && ( "ofNPAdjp" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Genitive np premodifying np
[ofNPNP] np np* -> np
	? { ( (1.End-0.Start) in 0.RB 
	    && "ofNPNP" in 0.AR
        )
		|| ( "PronNP" in 0.AR
              && 0.Case=="Genitive" 
		      && ! ( "ofNPNP" in 0.BR )
              && (1.End-0.Start) in 0.RB 
		    )
		 || ( "ofNPNP-1" in 1.AR
		    && (1.End-0.Start) in 0.RB
            )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Np pre-modified by pp	
[PpNp2Np] pp np* -> np
	? { (1.End-0.Start) in 0.RB
	  && ( "PP-NP" in 0.AR || "PpNp2Np" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

//Randall 10/29/08 for Tit1:16:1
// Adjp pre-modified by pp
[PP-Adjp] pp adjp* -> adjp
	? { (1.End-0.Start) in 0.RB
	  && "PP-Adjp" in 0.AR
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Np post-modified by pp	
[NP-Prep] np* prep -> pp
	? { (1.End-0.Start) in 0.RB
	  && "NP-Prep" in 0.AR
	  }
	>> { .Punc = 1.Punc; }

/* Np premodified by CL
Meant to apply relative clauses and infinitival clauses that modify np
*/
[CL-NP] CL np* -> np
	? { ( (1.End-0.Start) in 0.RB
		&& "CL-NP" in 0.AR
		)
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Adv modified by genitive np
[AdvofNP] adv* np -> advp
	? { "AdvofNP" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 1.Punc;
	   }

// Adjp premodified by adv	
[AdvpAdjp] advp adjp* -> adjp
	? { ( "AdvADJP" in 0.AR || "AdvpAdjp" in 0.AR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Pp premodified by adv (used in 130 cases, but inconsistently #)
[AdvPp] advp pp* -> pp
	? { ( ( 0.Lemma in {"kai/","ou)","ou)de/" } || 0.UnicodeLemma in {"καί","οὐ","οὐδέ","οὐδέ","οὐδέ", "καί" } )
	    && 0.Punc=="" 
	    && ! ( "AdvPP" in 0.BR || "AdvPp" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( (1.End-0.Start) in 0.RB && ( "AdvPP" in 0.AR || "AdvPp" in 0.AR ) )
	  || 0.UnicodeLemma in { "καί" } // to deal with Logos morph having adverbial KAI as ptcl
		 && 0.functionMorph == "BE"
		 && ( "advp2ADV " in 0.BR || "Advp2ADV" in 0.BR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// PP postmodified by adv Randall 10/27/08 for 1Th1:2:1
[PpAdvp] pp* advp -> pp
	? { ( "PPAdv" in 0.AR || "PpAdvp" in 0.AR )
		&& (1.End-0.Start) in 0.RB
	  }

// Cl premodifed by adv 
[AdvpCL] advp CL* -> CL
	? { "AdvpCL" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  && ! ( /* 0.Lemma in {"pri/n"} || */ 0.UnicodeLemma in {"πρίν"} ) // Logos morph has pri/n as conj rather than adv
      || "AdvpCLx" in 0.AR
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Adjp postmodified by advp	
[AdjpAdvp] adjp* advp -> adjp
	? { (1.End-0.Start) in 0.RB && "AdjpAdvp" in 0.AR }
	>> { .Punc = 1.Punc; }

// Advp premodified by adjp	  
[AdjpAdvp2Advp] adjp advp* -> advp
	? { (1.End-0.Start) in 0.RB && "AdjpAdvp2Advp" in 0.AR }
	>> { .Punc = 1.Punc; }
 
// Adjp premodified by dative np
[DativeAdjp] np adjp* -> adjp
	? { 0.Case == "Dative"
	  && ( "IOAdjp" in 0.AR || "CompAdjp" in 0.AR || "DativeAdjp" in 0.AR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc;
		 .RB = 0.RB; .NB = 0.NB;
	   }
/* Commented out & transferred to NpAdvp rule
// Np postmodified by dative np	   
[NpDative] np* np -> np
	? { 1.Case == "Dative"
	  && ( "NpIO" in 0.AR || "NpDative" in 0.AR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc;
	   }
*/

/* Commented out & transferred to AdvpNp rule
// Np premodified by dative np
[DativeNp] np np* -> np
	? { 0.Case == "Dative"
	  && ( "IONp" in 0.AR || "DativeNp" in 0.AR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc;
		 .RB = 0.RB; .NB = 0.NB;
	   }
*/
 
// Should particle AN be classified here (including moving AN often to make it work)?	#
// Why is the Relative value set to true? #
[PtclCL] ptcl CL* -> CL
	? { (1.End-0.Start) in 0.RB
//	  && 0.Punc=="" 
	  && "PtclCL" in 0.AR 
	  && ! ( (1.End-0.Start) in 0.NB )
	  || ( "sub-CL" in 0.AR || "that-VP" in 0.AR // && !Disjoint( {"sub-CL", "that-VP"}, 0.AR )
		|| ( 0.Lemma in {"ei)","e)a/n","o(/te","o(/tan","w(/ste","o(/pws","dio/ti"} || 0.UnicodeLemma in {"εἰ","ἐάν","ὅτε","ὅταν","ὥστε","ὅπως","διότι"} )
		 )		     
	  && ( (1.End-0.Start) in 0.RB 
		   || ( ( 0.Lemma in {"e)a/n"} || 0.UnicodeLemma in {"ἐάν"} ) && ! ( 1.Mood in {"Infinitive","Participle","Indicative"} ) )
		 )
	  && ! ( (1.End-0.Start) in 0.NB )
	  && ! ( "PtclCL" in 0.BR )
	  || ( 0.Lemma in {"a)mh/n"} || 0.UnicodeLemma in {"ἀμήν","ἀμήν"} ) // Automatic transformation of a)mh/n to PtclCL
		 && "Ptcl2Adv" in 0.AR
		 && ( 1.Lemma in {"le/gw"} || 1.UnicodeLemma in {"λέγω","λέγω"} )
	  || ( 0.Lemma in {"mh/"} || 0.UnicodeLemma in {"μή","μή"} )
		 && ( "that-VP" in 0.AR || "sub-CL" in 0.AR ) // convert mh/ introducing clauses to PtclCL
	     && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { // .Type="Relative"; 
	     .RB = 0.RB; .NB = 0.NB;
	   }


// RULES CONJOINING CLAUSE ELEMENTS

[Conj2P] P* conj P -> CL
	? { (2.End-0.Start) in 0.RB && "Conj2P" in 0.AR }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
	
[Conj6P] P* conj P conj P conj P conj P conj P -> CL
	? { (10.End-0.Start) in 0.RB && "Conj6P" in 0.AR }
	>> { .Punc = 10.Punc;
		 .Coord = true; }
	
[Conj2VP] vp* conj vp -> vp
	? { ( ( 1.Lemma in {"kai/", "a)lla/"} || 1.UnicodeLemma in {"καί", "ἀλλά", "ἀλλά", "καί"} ) // "and" "but"
	    && ! ( 0.Rule in {"Conj2VP"} )
	    && ! ( 2.Rule in {"Conj2VP"} )
	    && 0.Mood == 2.Mood
	    && ! ( 0.Rule == "Acc-VP" && 2.Rule == "VP-PP" )
	    && ! ( 0.Rule == "VP-Acc" && 2.Rule == "Acc-VP" )
	    && ! ( 0.Rule == "Nom-VP" && 2.Rule == "VP-Acc" )
	    && ! ( 0.UnicodeLemma in {"εἰμί","εἰμί"} && 0.Rule == "V2VP" ) // not just a "be"
	    && ! ( "Conj2VP" in 0.BR )
	    && (2.End-0.Start) in 0.RB
	    )
	  || ( (2.End-0.Start) in 0.RB && "Conj2VP" in 0.AR )
	  }
	>> { if ( 2.HasNom ) .HasNom = true; 
	     if ( 2.HasAcc ) .HasAcc = true; 
	     if ( 2.HasDat ) .HasDat = true; 
	     .HasConj = true;
         .ConjLemma = 1.Lemma;
	     .ConjLemma = 1.UnicodeLemma;
	     .Punc = 2.Punc;
		 .Coord = true; }
	  
[Conj3VP] vp* conj vp conj vp -> vp
	? { ( ( 1.Lemma in {"kai/"} || 1.UnicodeLemma in {"καί","καί"} ) // "and"
	    && ( 3.Lemma in {"kai/"} || 3.UnicodeLemma in {"καί","καί"} ) // "and"
//	    && 0.Mood == 1.Mood
//	    && 1.Mood == 2.Mood
        && ! ( 2.Rule == "Acc-VP" && 4.Rule == "VP-PP" )
	    && ! ( 0.UnicodeLemma in {"εἰμί","εἰμί"} && 0.Rule == "V2VP" ) // not just a "be"
	    && ! ( "Conj3VP" in 0.BR )
	    && (4.End-0.Start) in 0.RB
	    )
	  || ( (4.End-0.Start) in 0.RB && "Conj3VP" in 0.AR )
	  }
	>> { if ( 2.HasNom ) .HasNom = true; 
	     if ( 2.HasAcc ) .HasAcc = true; 
	     if ( 2.HasDat ) .HasDat = true;
	     if ( 4.HasNom ) .HasNom = true; 
	     if ( 4.HasAcc ) .HasAcc = true; 
	     if ( 4.HasDat ) .HasDat = true; 
	     .Mood = "Indicative";
	     .Punc = 2.Punc;
		 .Coord = true; }

[NpaNp] np* conj np -> np
	? { ( 0.Case == 2.Case 
	    && ( 1.Lemma in {"kai/","h)/"} || 1.UnicodeLemma in {"καί","ἤ","καί"} ) // "and" καί in N1904 is apparently a different font
	    && ! ( ( 0.Rule in { "NpPp", "QuanPp" } ) && 2.Rule == "N2NP" )
	    && ! ( 0.Rule in {"AdvpNp", "NpaNp", "Conj3Np", "Conj4Np"} )
	    && ! ( 2.Rule in {"AdvpNp", "NpaNp", "Conj3Np", "Conj4Np"} )
	    && ! ( "Conj2NP" in 0.BR || "NpaNp" in 0.BR )
		&& ! ( "Conj2NP-1" in 2.BR || "NpaNp-1" in 2.BR )
	    && (2.End-0.Start) in 0.RB
        && ! ( 0.Mood in {"Infinitive"} && "CL2S" in 0.AR && ( "Conj2CL" in 0.AR || "CLaCL" in 0.AR ))
        && ! ( 1.Mood in {"Infinitive"} && "CL2S" in 0.AR )
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "Conj2NP" in 0.AR || "NpaNp" in 0.AR ) && ! ( "Conj2NP-1" in 2.BR || "NpaNp-1" in 2.BR ) )
	  }
	>> { if ( 2.HasDet ) .HasDet = true;
	     if ( 2.HasPostPP ) .HasPostPP = true;
	     .Punc = 2.Punc;  
		 .Coord = true; }
   
[Conj3Np] np* conj np conj np -> np
	? { ( 0.Case == 2.Case
	    && 2.Case == 4.Case
	    && ! ( 0.Rule in {"NpaNp", "Conj3Np", "Conj4Np"} )
	    && ! ( 2.Rule in {"NpaNp", "Conj3Np", "Conj4Np"} )
	    && ! ( 4.Rule in {"NpaNp", "Conj3Np", "Conj4Np"} )
	    && ! ( "Conj3NP" in 0.BR || "Conj3Np" in 0.BR )
		&& ! ( 1.UnicodeLemma in {"οὐ","οὐ"} )
	    && (4.End-0.Start) in 0.RB
	    )
	  || ( (4.End-0.Start) in 0.RB && ( "Conj3NP" in 0.AR || "Conj3Np" in 0.AR ) )
	  }
	>> { .Punc = 4.Punc;
		 .Coord = true; }

// Randall 8/13/10 mat4:24:10
[2NpaNpaNp] np* np conj np conj np -> np
	? { 0.Case == 1.Case
	    && 1.Case == 3.Case
	    && 3.Case == 5.Case
	    && ( "NpNpaNpaNp" in 0.AR || "2NpaNpaNp" in 0.AR )
	    && (5.End-0.Start) in 0.RB
	  }
	>> { .Punc = 5.Punc;
		 .Coord = true; }
	   
[Conj4Np] np* conj np conj np conj np -> np
	? { ( 0.Case == 2.Case
	    && 2.Case == 4.Case
	    && 4.Case == 6.Case
	    && ! ( "Conj4NP" in 0.BR || "Conj4Np" in 0.BR )
	    && (6.End-0.Start) in 0.RB
	    )
	  || ( (6.End-0.Start) in 0.RB && ( "Conj4NP" in 0.AR || "Conj4Np" in 0.AR ) )
	  }
	>> { .Punc = 6.Punc;
		 .Coord = true; }
	
[Conj5Np] np* conj np conj np conj np conj np -> np
	? { ( "Conj5NP" in 0.AR || "Conj5Np" in 0.AR )
	  && ! ( (8.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 8.Punc;
		 .Coord = true; }
	
[Conj6Np] np* conj np conj np conj np conj np conj np -> np
	? { ( "Conj6NP" in 0.AR || "Conj6Np" in 0.AR )
	  && ! ( (10.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 10.Punc;
		 .Coord = true; }
	
[Conj7Np] np* conj np conj np conj np conj np conj np conj np -> np
	? { ( "Conj7NP" in 0.AR || "Conj7Np" in 0.AR )
	  && ! ( (12.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 12.Punc;
		 .Coord = true; }
	
[Conj8Np] np* conj np conj np conj np conj np conj np conj np conj np -> np
	? { ( "Conj8NP" in 0.AR || "Conj8Np" in 0.AR )
	  && ! ( (14.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 14.Punc;
		 .Coord = true; }
	
[Conj9Np] np* conj np conj np conj np conj np conj np conj np conj np conj np -> np
	? { ( "Conj9NP" in 0.AR || "Conj9Np" in 0.AR )
	  && ! ( (16.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 16.Punc;
		 .Coord = true; }
	
[Conj11Np] np* conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np -> np
	? { ( "Conj11NP" in 0.AR || "Conj11Np" in 0.AR )
	  && ! ( (20.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 20.Punc;
		 .Coord = true; }
	
[Conj12Np] np* conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np -> np
	? { ( "Conj12NP" in 0.AR || "Conj12Np" in 0.AR )
	  && ! ( (22.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 22.Punc;
		 .Coord = true; }
	
[Conj15NP] np* conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np conj np -> np
	? { "Conj15NP" in 0.AR
	  && ! ( (28.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 28.Punc;
		 .Coord = true; }

// Adjp modified by adjp (first adjp is head); not used when coordinate (may need new rule if such cases exist)
[AdjpAdjp] adjp* adjp -> adjp
	? { (1.End-0.Start) in 0.RB && "AdjpAdjp" in 0.AR }
	>> { .Punc = 1.Punc; }

// Logos has num for adj for numerals
[NumpNump] nump* nump -> nump
	? { (1.End-0.Start) in 0.RB
		&& ( "AdjpAdjp" in 0.AR || "NumpNump" in 0.AR )
	  }
	>> { .Punc = 1.Punc; }

// Randall 10/2/09 jhn21:11
[NumpNumpNump] nump* nump adjp -> nump
	? { (2.End-0.Start) in 0.RB
		&& ( "AdjpAdjpAdjp" in 0.AR || "3Adjp" in 0.AR || "NumpNumpNump" in 0.AR )
	  }
	>> { .Punc = 2.Punc; }

// Randall 10/5/09 act27:37:1
[NumpNumpNump2] adjp* nump nump -> nump
	? { (2.End-0.Start) in 0.RB
		&& ( "AdjpAdjpAdjp" in 0.AR || "3Adjp" in 0.AR || "NumpNumpNump2" in 0.AR )
	  }
	>> { .Punc = 2.Punc; }

// Randall 10/5/09 rev11:3:1
[NumpNumpNump3] adjp* adjp nump -> nump
	? { (2.End-0.Start) in 0.RB
		&& ( "AdjpAdjpAdjp" in 0.AR || "3Adjp" in 0.AR || "NumpNumpNump3" in 0.AR )
	  }
	>> { .Punc = 2.Punc; }

// Randall 10/3/09 luk2:34
[NumpAdjp] nump* adjp -> nump
	? { (1.End-0.Start) in 0.RB
		&& ( "AdjpAdjp" in 0.AR || "NumpAdjp" in 0.AR )
	  }
	>> { .Punc = 1.Punc; }

// Randall 10/1/09 jhn6:19
[AdjpNump] adjp* nump -> nump
	? { (1.End-0.Start) in 0.RB
		&& "AdjpNump" in 0.AR
		|| "AdjpAdjp" in 0.AR 
		&& (1.End-0.Start) in 0.RB
	  }
	>> { .Punc = 1.Punc; }

// Randall 9/18/09 mat18:22
[AdvpNump] advp* nump -> nump
	? { (0.Lemma in {"e(bdomhkonta/kis"} || 0.UnicodeLemma in {"ἑβδομηκοντάκις","ἑβδομηκοντάκις"} )
		&& ( 1.Lemma in {"e(pta/"} || 1.UnicodeLemma in {"ἑπτά","ἑπτά"} )
	  }
	>> { .Punc = 1.Punc; }

// Adjp modified by adjp (second adjp is head)
[AdjpAdjp2] adjp adjp* -> adjp
	? { (1.End-0.Start) in 0.RB && "AdjpAdjp2" in 0.AR }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 1.Punc;
	   }

// Three adjp in series (first is head)	
[3Adjp] adjp* adjp adjp -> adjp
	? { ( "AdjpAdjpAdjp" in 0.AR || "3Adjp" in 0.AR ) && ! ( (2.End-0.Start) in 0.NB) }
	>> { .Punc = 2.Punc;
		 .Coord = true; }

// Four adjp in series (first is head)	
[AdjpAdjpAdjpAdjp] adjp* adjp adjp adjp -> adjp
	? { "AdjpAdjpAdjpAdjp" in 0.AR && ! ( (3.End-0.Start) in 0.NB) }
	>> { .Punc = 3.Punc;
		 .Coord = true; }

// Five adjp in series (first is head)
[AdjpAdjpAdjpAdjpAdjp] adjp* adjp adjp adjp adjp -> adjp
	? { "AdjpAdjpAdjpAdjpAdjp" in 0.AR && ! ( (4.End-0.Start) in 0.NB) }
	>> { .Punc = 4.Punc;
		 .Coord = true; }

// Six adjp in series (first is head)	
[AdjpAdjpAdjpAdjpAdjpAdjp] adjp* adjp adjp adjp adjp adjp -> adjp
	? { "AdjpAdjpAdjpAdjpAdjpAdjp" in 0.AR && ! ( (5.End-0.Start) in 0.NB) }
	>> { .Punc = 5.Punc;
		 .Coord = true; }

//Randall 10/29/08 for Tit2:3:1
// Seven adjp in series (first is head)	
[AdjpAdjpAdjpAdjpAdjpAdjpAdjp] adjp* adjp adjp adjp adjp adjp adjp -> adjp
	? { "AdjpAdjpAdjpAdjpAdjpAdjpAdjp" in 0.AR && ! ( (6.End-0.Start) in 0.NB) }
	>> { .Punc = 6.Punc;
		 .Coord = true; }

// Used for two np's, not in apposition, conjoined without conj
[2Np] np* np -> np
	? { (1.End-0.Start) in 0.RB && ( "NpNp" in 0.AR || "2Np" in 0.AR )
	  && ! ( "NpNp-1" in 1.BR || "2Np-1" in 1.BR )
	  }
	>> { .Punc = 1.Punc;
		 .Coord = true; }

// Used for three np's in list, conjoined without conj	
[NpNpNp] np* np np -> np
	? { (2.End-0.Start) in 0.RB && "NpNpNp" in 0.AR
	  && ! ( "NpNpNp-2" in 2.BR )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
	
[NpNpNpNp] np* np np np -> np
	? { (3.End-0.Start) in 0.RB && "NpNpNpNp" in 0.AR }
	>> { .Punc = 3.Punc;
		 .Coord = true; }
	
[NpNpNpNpNp] np* np np np np -> np
	? { (4.End-0.Start) in 0.RB && "NpNpNpNpNp" in 0.AR }
	>> { .Punc = 4.Punc;
		 .Coord = true; }

[3NpaNp] np* np np conj np -> np
	? { (4.End-0.Start) in 0.RB && ( "NpNpNpAndNp" in 0.AR || "3NpaNp" in 0.AR ) }
	>> { .Punc = 4.Punc;
		 .Coord = true; }
		
[4NpaNp] np* np np np conj np -> np
	? { (5.End-0.Start) in 0.RB && ( "NpNpNpNpAndNp" in 0.AR || "4NpaNp" in 0.AR ) }
	>> { .Punc = 5.Punc;
		 .Coord = true; }
	
[NpNpNpNpNpNp] np* np np np np np -> np
	? { (5.End-0.Start) in 0.RB && "NpNpNpNpNpNp" in 0.AR }
	>> { .Punc = 5.Punc;
		 .Coord = true; }
	
[7Np] np* np np np np np np -> np
	? { (6.End-0.Start) in 0.RB && ( "NpNpNpNpNpNpNp" in 0.AR || "7Np" in 0.AR ) }
	>> { .Punc = 6.Punc;
		 .Coord = true; }
	
[NpNpNpNpNpNpNpNp] np* np np np np np np np -> np
	? { (7.End-0.Start) in 0.RB && "NpNpNpNpNpNpNpNp" in 0.AR }
	>> { .Punc = 7.Punc;
		 .Coord = true; }

[NpNpNpNpNpNpNpNpNp] np* np np np np np np np np -> np
	? { (8.End-0.Start) in 0.RB && "NpNpNpNpNpNpNpNpNp" in 0.AR }
	>> { .Punc = 8.Punc;
		 .Coord = true; }

//Doris 7/02/08 for 1tm1:8:1-1:11:11
[NpNpNpNpNpNpNpNpNpNp] np* np np np np np np np np np -> np
	? { (9.End-0.Start) in 0.RB && "NpNpNpNpNpNpNpNpNpNp" in 0.AR }
	>> { .Punc = 9.Punc;
		 .Coord = true; }

// Randall 6/24/09 mrk7:21:13-7:23:10
[12Np] np* np np np np np np np np np np np -> np
	? { (11.End-0.Start) in 0.RB && ( "NpNpNpNpNpNpNpNpNpNpNpNp" in 0.AR || "12Np" in 0.AR ) }
	>> { .Punc = 11.Punc;
		 .Coord = true; }

[NpNpNpNpNpNpNpNpNpNpNpNpNpNpNpAndNp] np* np np np np np np np np np np np np np np conj np -> np
	? { (16.End-0.Start) in 0.RB && "NpNpNpNpNpNpNpNpNpNpNpNpNpNpNpAndNp" in 0.AR }
	>> { .Punc = 16.Punc;
		 .Coord = true; }

//Randall 10/27/08 for 2Tm3:2:1
[NpNpNpNpNpNpNpNpNpNpNpNpNpNpNpNp] np* np np np np np np np np np np np np np np np -> np
	? { (15.End-0.Start) in 0.RB && "NpNpNpNpNpNpNpNpNpNpNpNpNpNpNpNp" in 0.AR }
	>> { .Punc = 15.Punc;
		 .Coord = true; }

//Randall 6/14/08 for Mat24:38
[VpVp] vp* vp -> vp
	? { "VpVp" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .Punc = 1.Punc;
		 .Coord = true; }
	
[VpVpandVp] vp* vp conj vp -> vp
	? { "VpVpandVp" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc;
		 .Coord = true; }
	
[AdjpaAdjp] adjp* conj adjp -> adjp
	? { ( 0.Case == 2.Case
	    && 0.Gender == 2.Gender
	    && 0.Number == 2.Number
	    && ! ( 0.Mood=="Participle" && 2.Mood=="Participle" )
	    && ! ( "Conj2AdjP" in 0.BR || "AdjpaAdjp" in 0.BR )
	    && (2.End-0.Start) in 0.RB
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "Conj2AdjP" in 0.AR || "AdjpaAdjp" in 0.AR ) )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }

// Randall 9/12/09 1tm5:19:1 Added as promoting num to adjp causes too many troubles than it is worth
[Conj2Nump3] nump* conj adjp -> nump
	? { (2.End-0.Start) in 0.RB 
		&& "Conj2Nump" in 0.AR
	  || ( (2.End-0.Start) in 0.RB && ( "Conj2AdjP" in 0.AR || "AdjpaAdjp" in 0.AR ) )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }

// Randall 9/21/09 gal3:17 Added as promoting num to adjp causes too many troubles than it is worth
[Conj2Nump2] adjp* conj nump -> nump
	? { (2.End-0.Start) in 0.RB 
		&& "Conj2Nump2" in 0.AR
	  || ( (2.End-0.Start) in 0.RB && ( "Conj2AdjP" in 0.AR || "AdjpaAdjp" in 0.AR ) )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }

// Randall 10/1/09 jhn2:20
[Conj2Nump] nump* conj nump -> nump
	? { (2.End-0.Start) in 0.RB 
		&& "Conj2Nump3" in 0.AR
	  || ( (2.End-0.Start) in 0.RB && ( "Conj2AdjP" in 0.AR || "AdjpaAdjp" in 0.AR ) )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
	
[AdvpaAdvp] advp* conj advp -> advp
	? { ( "Conj2Advp" in 0.AR || "AdvpaAdvp" in 0.AR ) && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
	
[Conj3Advp] advp* conj advp conj advp -> advp
	? { "Conj3Advp" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .Punc = 4.Punc;
		 .Coord = true; }
	
[ADVaADV] ADV* conj ADV -> ADV
	? { ( ( "Conj2ADV" in 0.AR || "ADVaADV" in 0.AR )
	    && ! ( (2.End-0.Start) in 0.NB )
	    ) 
	    || (
	        "ADVandADV" in 0.AR 
	        && ! ( (2.End-0.Start) in 0.NB )
	       )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
	
[Conj3ADV] ADV* conj ADV conj ADV -> ADV
	? { "Conj3ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .Punc = 4.Punc;
		 .Coord = true; }
	
[EitherOrAdjp] conj adjp* conj adjp -> adjp
	? { ( ( 0.Lemma=="ou)/te" && 2.Lemma=="ou)/te" || 0.UnicodeLemma=="οὔτε" && 2.UnicodeLemma=="οὔτε" )
	    && ! ( "EitherOrAdjp" in 0.BR )
	    )
	  || ( (3.End-0.Start) in 0.RB && "EitherOrAdjp" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 .Coord = true; }

[aNpaNp] conj np* conj np -> np
	? { ( ( 0.encode=="ou)/te" && 2.encode=="ou)/te" || 0.UnicodeLemma=="οὔτε" && 2.UnicodeLemma=="οὔτε" )
	    && ! ( "EitherOrNp" in 0.BR || "aNpaNp" in 0.BR )
	    )
	  || ( (3.End-0.Start) in 0.RB && ( "EitherOrNp" in 0.AR || "aNpaNp" in 0.AR ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 .Coord = true; }

[aNpaNpaNp] conj np* conj np conj np -> np
	? { ( "EitherOr3Np" in 0.AR || "aNpaNpaNp" in 0.AR ) && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 .Coord = true; }

[EitherOr4Np] conj np* conj np conj np conj np -> np
	? { "EitherOr4Np" in 0.AR && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
		 .Coord = true; }
	      
[EitherOr5Np] conj np* conj np conj np conj np conj np -> np
	? { "EitherOr5Np" in 0.AR && ! ( (9.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 9.Punc;
		 .Coord = true; }

[EitherOr8Np] conj np* conj np conj np conj np conj np conj np conj np conj np -> np
	? { "EitherOr8Np" in 0.AR && ! ( (15.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 15.Punc;
		 .Coord = true; }

// Randall 9/30/09 1co6:9
[EitherOr10Np] conj np* conj np conj np conj np conj np conj np conj np conj np conj np conj np -> np
	? { "EitherOr10Np" in 0.AR && ! ( (19.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 19.Punc;
		 .Coord = true; }
	   	   
[EitherOrVp] conj vp* conj vp -> vp
	? { (3.End-0.Start) in 0.RB && "EitherOrVp" in 0.AR
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 .Coord = true; }

[EitherOr3Vp] conj vp* conj vp conj vp -> vp
	? { "EitherOr3Vp" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 .Coord = true; }
	   
[EitherOr4Vp] conj vp* conj vp conj vp conj vp -> vp
	? { "EitherOr4Vp" in 0.AR && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
		 .Coord = true; }
	   
[EitherOr5Vp] conj vp* conj vp conj vp conj vp conj vp -> vp
	? { "EitherOr5Vp" in 0.AR && ! ( (9.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 9.Punc;
		 .Coord = true; }
	   	   
[aPpaPp] conj pp* conj pp -> pp
	? { (3.End-0.Start) in 0.RB && ( "EitherOrPp" in 0.AR || "aPpaPp" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 .Coord = true; }

[aPpaPpaPp] conj pp* conj pp conj pp -> pp
	? { ( "EitherOr3Pp" in 0.AR || "aPpaPpaPp" in 0.AR ) && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 .Coord = true; }
	   
[EitherOr4Pp] conj pp* conj pp conj pp conj pp -> pp
	? { "EitherOr4Pp" in 0.AR && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
		 .Coord = true; }

// Randall 8/27/09 2pe3:18:14
[EitherAdvpOrPp] conj advp* conj pp -> advp
	? { (3.End-0.Start) in 0.RB && "EitherAdvpOrPp" in 0.AR
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 .Coord = true; }
	   	   
[aAdvpaAdvp] conj advp* conj advp -> advp
	? { (3.End-0.Start) in 0.RB && ( "EitherOrAdvp" in 0.AR || "aAdvpaAdvp" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 .Coord = true; }
	   
[EitherOr4Advp] conj advp* conj advp conj advp conj advp -> advp
	? { "EitherOr4Advp" in 0.AR && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
		 .Coord = true; }
	   
[Conj3Adjp] adjp* conj adjp conj adjp -> adjp
	? { ( 0.Case == 2.Case
	    && 0.Case == 4.Case
	    && 0.Gender == 2.Gender
	    && 0.Gender == 4.Gender
	    && 0.Number == 2.Number
	    && 0.Number == 4.Number
	    && ! ( 0.Mood=="Participle" && 2.Mood=="Participle" && 4.Mood=="Participle" )
	    && ! ( "Conj3AdjP" in 0.BR || "Conj3Adjp" in 0.BR )
	    && (4.End-0.Start) in 0.RB
	    )
	  || ( (4.End-0.Start) in 0.RB && ( "Conj3AdjP" in 0.AR || "Conj3Adjp" in 0.AR ) )
	  }
	>> { .Punc = 4.Punc;
		 .Coord = true; }
	
[Conj4Adjp] adjp* conj adjp conj adjp conj adjp -> adjp
	? { ( "Conj4AdjP" in 0.AR || "Conj4Adjp" in 0.AR ) && ! ( (6.End-0.Start) in 0.NB ) }
	>> { .Punc = 6.Punc;
		 .Coord = true; }
	
[Conj5AdjP] adjp* conj adjp conj adjp conj adjp conj adjp -> adjp
	? { "Conj5AdjP" in 0.AR && ! ( (8.End-0.Start) in 0.NB ) }
	>> { .Punc = 8.Punc;
		 .Coord = true; }

[Conj2Pp] pp* conj pp -> pp
	? { ( ( 1.Lemma in {"kai/","h)/","ou)de/"} || 1.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ! ( "Conj2PP" in 0.BR || "Conj2Pp" in 0.BR )
	    && ! ( "Conj2PP-2" in 2.BR || "Conj2Pp-2" in 2.BR )
	    && (2.End-0.Start) in 0.RB
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "Conj2PP" in 0.AR || "Conj2Pp" in 0.AR ) && ! ( "Conj2PP-2" in 2.BR || "Conj2Pp-2" in 2.BR ) )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
	
[notPPbutPP] advp pp conj pp* -> pp
	? { ( ( 0.Lemma=="ou)" && 2.Lemma=="a)lla/" || 0.UnicodeLemma=="οὐ" && 2.UnicodeLemma in {"ἀλλά","ἀλλά"} )
	    && ! ( "notPPbutPP" in 0.BR )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || "notPPbutPP" in 0.AR
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Coord = true; }
	
[notNPbutNP] advp np conj np* -> np
	? { "notNPbutNP" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Coord = true; }
	
[notADVbutADV] advp ADV conj ADV* -> ADV
	? { "notADVbutADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Coord = true; }
	
[notVPbutVP] advp vp conj vp* -> vp
	? { "notVPbutVP" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Coord = true; }
	
[notCLbutCL] advp CL conj CL* -> vp
	? { "notCLbutCL" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Coord = true; }
	
[notCLbutCL2CL] advp CL conj CL* -> CL
	? { "notCLbutCL2CL" in 0.AR 
		&& ! ( (3.End-0.Start) in 0.NB )
		&& ! ( 2.UnicodeLemma in {"ὅτι","ἵνα","καί","καθώς","καί","καθώς"} ) // address both JT & SBL morph font differences
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Coord = true; }

/* Removed as redundant
[ADVandADV] ADV* conj ADV -> ADV
	? { "ADVandADV" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
*/

// Should be used only when two adv's coordinate, not one modifying the other (use 2Advp_h1 for that)
[AdvAdv] ADV* ADV -> ADV
	? { "AdvAdv" in 0.AR 
	  && ! ( 0.Rule in {"AdvAdv"} || 1.Rule in {"AdvAdv"} )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc;
		 .Coord = true; }
		  
[2Pp] pp* pp -> pp
	? { ( "PpPp" in 0.AR || "PP-PP" in 0.AR || "2Pp" in 0.AR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc;
		 .Coord = true; }
	
[PpPpPp] pp* pp pp -> pp
	? { ( "PpPpPp" in 0.AR || "PP-PP-PP" in 0.AR )
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
	
[PpPpPpPp] pp* pp pp pp -> pp
	? { ( "PpPpPpPp" in 0.AR || "PP-PP-PP-PP" in 0.AR )
	  && ! ( (3.End-0.Start) in 0.NB )
      && ! ( "PpPpPpPp-1" in 3.BR )
	  }
	>> { .Punc = 3.Punc;
		 .Coord = true; }
	
[PpPpPpPpPp] pp* pp pp pp pp -> pp
	? { "PpPpPpPpPp" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc;
		 .Coord = true; }
	
[PpPpPpPpPpPp] pp* pp pp pp pp pp -> pp
	? { "PpPpPpPpPpPp" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc;
		 .Coord = true; }

// Randall 6/5/08 for 1Co6:2:13 (old)
[PpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPp] pp* pp pp pp pp pp pp pp pp pp pp pp pp pp pp pp -> pp
	? { "PpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPp" in 0.AR 
	  && ! ( (15.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 15.Punc;
		 .Coord = true; }

// Randall 8/17/09 1co6:2:13 (new)
[PpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPp] pp* pp pp pp pp pp pp pp pp pp pp pp pp pp pp pp pp pp pp pp pp -> pp
	? { "PpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPpPp" in 0.AR 
	  && ! ( (20.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 20.Punc;
		 .Coord = true; }
		
[2PpaPp] pp* pp conj pp -> pp
	? { ( ( 2.Lemma in {"kai/","h)/","ou)de/"} || 2.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ! ( "PpPpaPp" in 0.BR || "2PpaPp" in 0.BR )
	    && (3.End-0.Start) in 0.RB
	    )
	  || ( (3.End-0.Start) in 0.RB && ( "PpPpaPp" in 0.AR || "2PpaPp" in 0.AR ) )
	  }
	>> { .Punc = 3.Punc;
		 .Coord = true; }

[Conj3Pp] pp* conj pp conj pp -> pp
	? { ( ( 1.Lemma in {"kai/","h)/","ou)de/"} || 1.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 3.Lemma in {"kai/","h)/","ou)de/"} || 3.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ! ( "Conj3PP" in 0.BR || "Conj3Pp" in 0.BR )
	    && (4.End-0.Start) in 0.RB
	    )
	  || ( (4.End-0.Start) in 0.RB && ( "Conj3PP" in 0.AR || "Conj3Pp" in 0.AR ) )
	  }	
	>> { .Punc = 4.Punc;
		 .Coord = true; }
	
[Conj4Pp] pp* conj pp conj pp  conj pp -> pp
	? { ( ( 1.Lemma in {"kai/","h)/","ou)de/"} || 1.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 3.Lemma in {"kai/","h)/","ou)de/"} || 3.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 5.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ! ( "Conj4PP" in 0.BR || "Conj4Pp" in 0.BR )
	    && (6.End-0.Start) in 0.RB
	    )
	  || ( (6.End-0.Start) in 0.RB && ( "Conj4PP" in 0.AR || "Conj4Pp" in 0.AR ) )
	  }	
	>> { .Punc = 6.Punc;
		 .Coord = true; }
	
[Conj5Pp] pp* conj pp conj pp conj pp conj pp -> pp
	? { ( "Conj5PP" in 0.AR || "Conj5Pp" in 0.AR ) && ! ( (8.End-0.Start) in 0.NB ) 
        && ( 1.Lemma in {"kai/","h)/","ou)de/"} || 1.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 3.Lemma in {"kai/","h)/","ou)de/"} || 3.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 5.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
	    && ( 7.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
      }
	>> { .Punc = 8.Punc;
		 .Coord = true; }
	
[Conj6Pp] pp* conj pp conj pp conj pp conj pp conj pp -> pp
	? { ( "Conj6PP" in 0.AR || "Conj6Pp" in 0.AR ) && ! ( (10.End-0.Start) in 0.NB ) 
        && ( 1.Lemma in {"kai/","h)/","ou)de/"} || 1.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 3.Lemma in {"kai/","h)/","ou)de/"} || 3.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 5.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
	    && ( 7.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
	    && ( 9.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
      }
	>> { .Punc = 10.Punc;
		 .Coord = true; }
	
[Conj7Pp] pp* conj pp conj pp conj pp conj pp conj pp conj pp -> pp
	? { ( "Conj7PP" in 0.AR || "Conj7Pp" in 0.AR ) && ! ( (12.End-0.Start) in 0.NB ) 
        && ( 1.Lemma in {"kai/","h)/","ou)de/"} || 1.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 3.Lemma in {"kai/","h)/","ou)de/"} || 3.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or"
	    && ( 5.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
	    && ( 7.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
	    && ( 9.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
	    && ( 11.Lemma in {"kai/","h)/","ou)de/"} || 5.UnicodeLemma in {"καί","ἤ","οὐδέ","οὐδέ","καί"} ) // "and" "or
      }
	>> { .Punc = 12.Punc;
		 .Coord = true; }

/* Commented out as problematic because child node is terminal label ptcl; each ptcl modifies the clause that follows, not one another	   
[PtclPtcl] ptcl* ptcl -> ptcl
	? { ! ( "PtclPtcl" in 0.BR ) 
	  && 0.Punc=="" 
	  }
	>> { .Punc = 1.Punc;
		 .Coord = true; }
*/

/* Commented out as problematic because child node is terminal label ptcl; each ptcl modifies the clause that follows, not one another	   	
[PtclPtclPtcl] ptcl* ptcl ptcl-> ptcl
	? { ! ( "PtclPtclPtcl" in 0.BR ) 
	  && 0.Punc==""
	  && 1.Punc=="" 
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }
*/

// Should not be used when two adv's are coordinate (use AdvAdv for that)
[2Advp_h1] advp* advp -> advp
	? { ( ( 0.Lemma in {"ou)de/","ou)","mh/"} || 0.UnicodeLemma in {"οὐδέ","οὐδέ","οὐ","μή","μή"} ) // "οὐδέ" added because this erroneous form in SBLGNT
	    && ( 1.Lemma in {"ou)de/","ou)","mh/"} || 1.UnicodeLemma in {"οὐδέ","οὐδέ","οὐ","μή","μή"} )
	    && ! ( "AdvpAdvp" in 0.AR || "2Advp_h1" in 0.AR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( ( "AdvpAdvp" in 0.AR || "2Advp_h1" in 0.AR ) && ! ( (1.End-0.Start) in 0.NB ) )
	  || ( "AdjpAdvp" in 0.AR || "2Advp_h1" in 0.AR ) // mrk1:35 for AdjpAdvp that turned to 2Advp_h1 in Logos morph
		 && "adjp2ADV" in 0.AR
		 && (1.End-0.Start) in 0.RB
	  }
	>> { .Punc = 1.Punc; }
	
[2Advp_h2] advp advp* -> advp
	? { ( "AdvpAdvp2" in 0.AR || "2Advp_h2" in 0.AR ) && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 1.Punc; 
	   }
	
[AdvpAdvpAdvp] advp* advp advp -> advp
	? { "AdvpAdvpAdvp" in 0.AR
	  && (2.End-0.Start) in 0.RB
	  }
	>> { .Punc = 2.Punc;
		 .Coord = true; }


// CLAUSE LEVEL RULES

[Np2CL] np* -> CL
	? { ( 0.Case=="Vocative"
	    && ! ( 0.Rule in {"CL2NP"} )
	    && ! ( "np2CL" in 0.BR || "Np2CL" in 0.BR )
	    )
	  || ( ( "np2CL" in 0.AR || "Np2CL" in 0.AR )
		 && ! ( 0.Rule in {"CL2NP"} ) 
		 && ! ( 0.Lemma in { "i)dou/", "i)/de" } )
		 && ! ( 0.UnicodeLemma in { "ἰδού","ἴδε" } )
		 && ! ( 0.Lemma in {"nai/"} || 0.UnicodeLemma in {"ναί"} )
		 )
	  }
	>> { .ClType = "Minor"; }


[Intj2CL] intj* -> CL
	? { "Intj2CL" in 0.AR
		&& ! ( 0.Lemma in {"ou)"} || 0.UnicodeLemma in {"οὐ"} )
		|| ( 0.Lemma in { "i)dou/", "i)/de" } || 0.UnicodeLemma in { "ἰδού","ἴδε" } )
		|| ( "Intj2V" in 0.AR
			&& ( 0.Lemma in { "i)dou/", "i)/de" } || 0.UnicodeLemma in { "ἰδού","ἴδε" } )
		|| ( 0.Lemma in {"nai/"} || 0.UnicodeLemma in {"ναί"} )
		   && "Ptcl2Np" in 0.AR
		   && ( "np2CL" in 0.AR || "Np2CL" in 0.AR )
		   )
	  }	
	>> { .ClType = "Minor"; }

/* This rule was used 8 times, all erroneously and so were commented out when all changed
[Adjp2CL] adjp* -> CL
	? { ( "adjp2CL" in 0.AR || "Adjp2CL" in 0.AR ) }
*/

[V2CL] V* -> CL
	? { ! ( "V2CL" in 0.BR )
        && ! ( "V2CLx" in 0.BR ) // Randall 7/6/11 mrk12:27
	  || 0.Mood in {"Participle"}
        && ! ( "V2CLx" in 0.BR ) // Randall 7/6/11 mrk12:27
//    || 0.Lemma in { "i)dou/", "i)/de" }
//    || 0.UnicodeLemma in { "ἰδού","ἴδε" }
	  }

// Randall 6/1/09 1jn3:1:12-3:1:13
[VC2CL] VC* -> CL
	? { ( "VC2CL" in 0.AR ) }
	
[V-S] V* S -> CL
	? { ( 0.Number == 1.Number
	    && ! ( "V-S" in 0.BR ) 
	    && 0.Punc=="" 
	    && ! ( (1.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( "V-S" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 1.Punc;
	   }
	
[V-O-S] V* O S -> CL
	? { ( 0.Number == 2.Number
	    && ! ( "V-O-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "V-O-S" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc;
	   }
	
[V-O-S-ADV] V* O S ADV -> CL
	? { ("V-O-S-ADV" in 0.AR || "V-O-S-PP" in 0.AR)
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc;
	   }
	
[V-O-S-ADV-ADV] V* O S ADV ADV -> CL
	? { "V-O-S-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc;
	   }
	
[V-O-S-ADV-ADV-ADV] V* O S ADV ADV ADV -> CL
	? { "V-O-S-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc;
	   }

[V-ADV-O-S] V* ADV O S -> CL
	? { "V-ADV-O-S" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc;
	   }
		
[V-ADV-O-S-ADV] V* ADV O S ADV -> CL
	? { "V-ADV-O-S-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-O-S-IO] V* O S IO -> CL
	? { "V-O-S-IO" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }

[V-O-S-O2] V* O S O2 -> CL
	? { "V-O-S-O2" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }

// Randall 7/13/09 mrk4:10:1
[V-O-S-ADV-O2] V* O S ADV O2 -> CL
	? { "V-O-S-ADV-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

// Randall 8/25/11 jhn18:24
[V-O-S-O2-ADV] V* O S O2 ADV -> CL
	? { "V-O-S-O2-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

// Randall 9/22/10 2th2:13
[V-O-S-O2-ADV-ADV] V* O S O2 ADV ADV -> CL
	? { "V-O-S-O2-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }

//Randall 4/8/09 for Act7:35:13
[O-S-O2-V-ADV] O S O2 V* ADV -> CL
	? { "O-S-O2-V-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .RB = 0.RB; .NB = 0.NB;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }

//Randall 8/22/08 for Act5:31:1
[O-S-O2-V-ADV-ADV] O S O2 V* ADV ADV -> CL
	? { "O-S-O2-V-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; 
	     .RB = 0.RB; .NB = 0.NB;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	
[O-S-O2-V-IO-ADV] O S O2 V* IO ADV -> CL
	? { "O-S-O2-V-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; 
	     .RB = 0.RB; .NB = 0.NB;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	   
[O-S-ADV-V-IO-ADV] O S ADV V* IO ADV -> CL
	? { "O-S-ADV-V-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; 
	     .RB = 0.RB; .NB = 0.NB;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	
[V-O-S-IO-ADV] V* O S IO ADV -> CL
	? { "V-O-S-IO-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

	
[ADV-V-O-ADV-S-IO-ADV] ADV V* O ADV S IO ADV -> CL
	? { "ADV-V-O-ADV-S-IO-ADV" in 0.AR 
	  && (6.End-0.Start) in 0.RB
	  }
	>> { .Punc = 6.Punc;
	     .RB = 0.RB; .NB = 0.NB;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	
[V-S-IO] V* S IO -> CL
	? { ( 0.Number == 1.Number
	    && ! ( "V-S-IO" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" 
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( "V-S-IO" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; }
	
[ADV-V-ADV-S-IO] ADV V* ADV S IO -> CL
	? { "ADV-V-ADV-S-IO" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc;
		 .RB = 0.RB; .NB = 0.NB;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	
[V-S-ADV-ADV-IO] V* S ADV ADV IO -> CL
	? { "V-S-ADV-ADV-IO" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-S-IO-ADV] V* S IO ADV -> CL
	? { "V-S-IO-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-S-IO-ADV-ADV] V* S IO ADV ADV -> CL
	? { "V-S-IO-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-S-IO-O] V* S IO O -> CL
	? { ( 0.Number == 1.Number
	    && ! ( "V-S-IO-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( "V-S-IO-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 3.Punc; }

[V-S-IO-O-O2] V* S IO O O2 -> CL
	? { "V-S-IO-O-O2" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

[V-S-IO-O2-O] V* S IO O2 O -> CL
	? { "V-S-IO-O2-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

[V-ADV-O-ADV] V* ADV O ADV -> CL
	? { ( "V-ADV-O-ADV" in 0.AR || "V-PP-O-PP" in 0.AR || "V-PP-O-ADV" in 0.AR || "V-PP-O-PP" in 0.AR )
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-O-ADV-ADV] V* ADV O ADV ADV -> CL
	? { "V-ADV-O-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-O-ADV-ADV-ADV] V* ADV O ADV ADV ADV -> CL
	? { "V-ADV-O-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }

[V-ADV-ADV-O] V* ADV ADV O -> CL
	? { "V-ADV-ADV-O" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }

// Randall 6-4-08 for Act11:26:6
[V-ADV-ADV-O-O2] V* ADV ADV O O2 -> CL
	? { "V-ADV-ADV-O-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-ADV-ADV-O] V* ADV ADV ADV O -> CL
	? { "V-ADV-ADV-ADV-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-ADV-ADV] V* ADV ADV ADV -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "V-ADV-ADV-ADV" in 0.BR || "V-PP-PP-ADV" in 0.BR || "V-ADV-PP-PP" in 0.BR )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  ||  ( ( "V-ADV-ADV-ADV" in 0.AR || "V-PP-PP-ADV" in 0.AR || "V-ADV-PP-PP" in 0.AR )
	      && ! ( (3.End-0.Start) in 0.NB )
	      )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-ADV-ADV-ADV] V* ADV ADV ADV ADV -> CL
	? { "V-ADV-ADV-ADV-ADV" in 0.AR
	  && (4.End-0.Start) in 0.RB
	  }
	>> { .Punc = 4.Punc; }

[V-ADV-S-O] V* ADV S O -> CL
	? { "V-ADV-S-O" in 0.AR 
	     && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[ADV-V-ADV-S-O] ADV V* ADV S O -> CL
	? { "ADV-V-ADV-S-O" in 0.AR 
	     && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	
[V-ADV-S-O-ADV] V* ADV S O ADV -> CL
	? { "V-ADV-S-O-ADV" in 0.AR 
	     && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-ADV-S-O] V* ADV ADV S O -> CL
	? { "V-ADV-ADV-S-O" in 0.AR 
	     && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-S-ADV] V* ADV S ADV -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && 0.Number == 2.Number
	    && ! ( "V-ADV-S-ADV" in 0.BR || "V-PP-S-PP" in 0.BR || "V-ADV-S-PP" in 0.BR )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-ADV-S-ADV" in 0.AR || "V-PP-S-PP" in 0.AR || "V-ADV-S-PP" in 0.AR)
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-S-ADV-O] V* ADV S ADV O -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && 0.Number == 2.Number
	    && ! ( "V-ADV-S-ADV-O" in 0.BR || "V-PP-S-PP-O" in 0.BR)
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-ADV-S-ADV-O" in 0.AR || "V-PP-S-PP-O" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 4.Punc; }
	  	  
[V-S-O] V* S O -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( "V-S-O" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( "V-S-O" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; }  

//Randall 8/31/08 for act25:11:14
[O2-S-V-O] O2 S V* O -> CL
	? { "O2-S-V-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }

[O2-V-S-O] O2 V* S O -> CL
	? { "O2-V-S-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }

[V-S-O-ADV] V* S O ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "V-S-O-ADV" in 0.BR || "V-S-O-PP" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( ( "V-S-O-ADV" in 0.AR || "V-S-O-PP" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 3.Punc; }
	
[V-S-O-ADV-ADV] V* S O ADV ADV -> CL
	? { "V-S-O-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

[V-S-O-IO] V* S O IO -> CL
	? { "V-S-O-IO" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }

// Randall 6/9/09 gal4:4:1-4:5:9
[V-S-O-O2] V* S O O2 -> CL
	? { "V-S-O-O2" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-S-ADV-O-IO] V* S ADV O IO -> CL
	? { "V-S-ADV-O-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-S-O-IO-ADV] V* S O IO ADV -> CL
	? { "V-S-O-IO-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[S-O-IO-V-ADV] S O IO V* ADV -> CL
	? { "S-O-IO-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .RB = 0.RB; .NB = 0.NB;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	   	   
[ADV-V-S-ADV] ADV V* S ADV -> CL
	? { ( ( "ADV-V-S-ADV" in 0.AR || "PP-V-S-PP" in 0.AR || "PP-V-S-ADV" in 0.AR || "ADV-V-S-PP" in 0.AR )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }

[ADV-V-S-IO] ADV V* S IO -> CL
	? { "ADV-V-S-IO" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	      
[ADV-V-S-IO-ADV] ADV V* S IO ADV -> CL
	? { "ADV-V-S-IO-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }	   
  
[ADV-V-O-ADV-ADV] ADV V* O ADV ADV -> CL
	? { ( ( "PP-V-O-PP-PP" in 0.AR || "ADV-V-O-ADV-ADV" in 0.AR || "ADV-V-O-PP-PP" in 0.AR )
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }	   
	   
[ADV-V-O-ADV-ADV-ADV] ADV V* O ADV ADV ADV -> CL
	? { "ADV-V-O-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }	   

[ADV-V-O-IO] ADV V* O IO -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "PP-V-O-IO" in 0.BR || "ADV-V-O-IO" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "PP-V-O-IO" in 0.AR || "ADV-V-O-IO" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	   	 		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative") )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-O-IO] ADV ADV V* O IO -> CL
	? { "ADV-ADV-V-O-IO" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }	   

//Doris 8/21/08 for Rom6:19:9-6:19:34
[ADV-ADV-V-O-O2-IO-ADV] ADV ADV V* O O2 IO ADV -> CL
	? { "ADV-ADV-V-O-O2-IO-ADV" in 0.AR
		&& ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-O-IO-ADV] ADV V* O IO ADV -> CL
	? { "ADV-V-O-IO-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   	      
[ADV-V-ADV-ADV] ADV V* ADV ADV -> CL
	? { ( ( "PP-V-PP-PP" in 0.AR || "ADV-V-ADV-ADV" in 0.AR || "PP-V-ADV-PP" in 0.AR || "ADV-V-ADV-PP" in 0.AR || "ADV-V-PP-PP" in 0.AR )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-IO] ADV V* ADV IO -> CL
	? { "ADV-V-ADV-IO" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-IO-O] ADV V* ADV IO O -> CL
	? { "ADV-V-ADV-IO-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[V-S-ADV] V* S ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( "V-S-ADV" in 0.BR || "V-S-PP" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( ( "V-S-ADV" in 0.AR || "V-S-PP" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; } 
	
[V-S-ADV-ADV] V* S ADV ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "V-S-ADV-ADV" in 0.BR || "V-S-ADV-PP" in 0.BR || "V-S-PP-PP" in 0.BR || "V-S-PP-ADV" in 0.BR) 
	    && ! ( (3.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( ( "V-S-ADV-ADV" in 0.AR || "V-S-ADV-PP" in 0.AR || "V-S-PP-PP" in 0.AR || "V-S-PP-ADV" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 3.Punc; }
	
[V-S-ADV-ADV-ADV] V* S ADV ADV ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( "V-S-ADV-ADV-PP" in 0.BR || "V-S-ADV-ADV-ADV" in 0.BR || "V-S-PP-PP-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( ( "V-S-ADV-ADV-PP" in 0.AR || "V-S-ADV-ADV-ADV" in 0.AR || "V-S-PP-PP-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 4.Punc; }
	  
[V-S-ADV-O] V* S ADV O -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "V-S-ADV-O" in 0.BR || "V-S-PP-O" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 1.Case in {"Nominative"} )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 1.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 1.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 1.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 1.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( ( "V-S-ADV-O" in 0.AR || "V-S-PP-O" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 3.Punc; } 
	
[V-S-ADV-ADV-O] V* S ADV ADV O -> CL
	? { "V-S-ADV-ADV-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; } 
	
[V-S-ADV-O-ADV] V* S ADV O ADV -> CL
	? { "V-S-ADV-O-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-S-ADV-O-ADV-ADV] V* S ADV O ADV ADV -> CL
	? { "V-S-ADV-O-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }
	
[ADV-V] ADV V* -> CL
	? { ( ! ( "ADV-V" in 0.BR || "PP-V" in 0.BR ) 
	    && 0.Punc==""
	    && ! ( (1.End-0.Start) in 0.NB )
        && ! ( 1.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 0.UnicodeLemma in {"λέγω"} && 0.Mood in {"Infinitive"} )
	    )
	  || ( ( "ADV-V" in 0.AR || "PP-V" in 0.AR )
	     && ! ( (1.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 1.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	 
[ADV-ADV-O-V] ADV ADV O V* -> CL
	? { ( "ADV-ADV-O-V" in 0.AR 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-O-V-IO] ADV ADV O V* IO -> CL
	? { ( "ADV-ADV-O-V-IO" in 0.AR 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-O-V-S] ADV ADV O V* S -> CL
	? { ( "ADV-ADV-O-V-S" in 0.AR 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 10/5/09 act17:14:1
[ADV-ADV-O-V-S-ADV] ADV ADV O V* S ADV -> CL
	? { ( "ADV-ADV-O-V-S-ADV" in 0.AR 
	    && ! ( (5.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 1/28/14 for N1904 mat5:25
[ADV-ADV-O-V-S-IO] ADV ADV O V* S IO -> CL
	? { ( "ADV-ADV-O-V-S-IO" in 0.AR 
	    && ! ( (5.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-O-V] ADV ADV ADV O V* -> CL
	? { ( "ADV-ADV-ADV-O-V" in 0.AR 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-S-O-V] ADV ADV ADV S O V* -> CL
	? { ( "ADV-ADV-ADV-S-O-V" in 0.AR 
	    && ! ( (5.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	     
[ADV-ADV-O-V-ADV] ADV ADV O V* ADV -> CL
	? { ( "ADV-PP-O-V-PP" in 0.AR || "ADV-ADV-O-V-ADV" in 0.AR
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-O-V-ADV-ADV] ADV ADV O V* ADV ADV -> CL
	? { "ADV-ADV-O-V-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-V-ADV] ADV V* ADV -> CL
	? { ( ! ( "ADV-V-ADV" in 0.BR || "PP-V-PP" in 0.BR || "PP-V-ADV" in 0.BR || "ADV-V-PP" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 1.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 1.UnicodeLemma in {"λέγω"} && 2.Rule in {"CL2ADV"} && 2.Case in {"Nominative"} )
	    )
	  || ( ( "ADV-V-ADV" in 0.AR || "PP-V-PP" in 0.AR || "PP-V-ADV" in 0.AR || "ADV-V-PP" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 2.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-O] ADV V* ADV O -> CL
	? {  ( ( "ADV-V-ADV-O" in 0.AR || "ADV-V-PP-O" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-V-ADV-O-S] ADV V* ADV O S -> CL
	? {  ( ( "ADV-V-ADV-O-S" in 0.AR || "ADV-V-PP-O-S" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-ADV-O] ADV ADV V* ADV O -> CL
	? { "ADV-ADV-V-ADV-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
   
[ADV-ADV-V-ADV-ADV-O] ADV ADV V* ADV ADV O -> CL
	? { "ADV-ADV-V-ADV-ADV-O" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-O-IO] ADV V* ADV O IO -> CL
	? { "ADV-V-ADV-O-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-O-ADV] ADV V* ADV O ADV  -> CL
	? {  "ADV-V-ADV-O-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 12/24/08 mat27:7:1 40027007001 Matthew's request for alternate tree
[ADV-V-ADV-O-ADV-IO] ADV V* ADV O ADV IO -> CL
	? {  "ADV-V-ADV-O-ADV-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-O-ADV-ADV] ADV V* ADV O ADV ADV -> CL
	? {  "ADV-V-ADV-O-ADV-ADV" in 0.AR 
	  && (5.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-S] ADV V* ADV S -> CL
	? { ( "ADV-V-ADV-S" in 0.AR || "ADV-V-PP-S" in 0.AR || "PP-V-ADV-S" in 0.AR ) 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-V-IO] ADV V* IO -> CL
	? { ( ! ( "ADV-V-IO" in 0.BR || "PP-V-IO" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-IO" in 0.AR || "PP-V-IO" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 2.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-S] ADV V* IO S -> CL
	? { ( ! ( "ADV-V-IO-S" in 0.BR ) 
	    && 1.Number == 3.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-V-IO-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-ADV-S] ADV V* IO ADV S -> CL
	? { "ADV-V-IO-ADV-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-S-ADV] ADV V* IO S ADV -> CL
	? { "ADV-V-IO-S-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-S-O] ADV V* IO S O -> CL
	? { "ADV-V-IO-S-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 3/7/14 for N1904 2tm2:24
[ADV-ADV-V-IO-S-O-ADV] ADV ADV V* IO S O ADV-> CL
	? { "ADV-ADV-V-IO-S-O-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

//Doris 6/17/08 for 2Th2:11:1-2:12:12
[ADV-V-IO-S-O-ADV] ADV V* IO S O ADV-> CL
	? { "ADV-V-IO-S-O-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-O] ADV V* IO O -> CL
	? { ( ! ( "ADV-V-IO-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-V-IO-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-IO-O] ADV ADV V* IO O -> CL
	? { "ADV-ADV-V-IO-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-O-ADV] ADV V* IO O ADV -> CL
	? { "ADV-V-IO-O-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-ADV] ADV V* IO ADV -> CL
	? { ( ! ( "ADV-V-IO-PP" in 0.BR || "ADV-V-IO-ADV" in 0.BR  ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-IO-PP" in 0.AR || "ADV-V-IO-ADV" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-ADV-ADV] ADV V* IO ADV ADV -> CL
	? { "ADV-V-IO-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 7/6/09 luk1:67:1-1:75:10
[ADV-V-IO-ADV-ADV-ADV] ADV V* IO ADV ADV ADV -> CL
	? { "ADV-V-IO-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-IO-ADV-O] ADV V* IO ADV O -> CL
	? { "ADV-V-IO-ADV-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
	   }
	      
[ADV-V-ADV-ADV-S-ADV] ADV V* ADV ADV S ADV -> CL
	? {  "ADV-V-ADV-ADV-S-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-ADV-S-ADV] ADV V* ADV S ADV -> CL
	? {  "ADV-V-PP-S-ADV" in 0.AR || "ADV-V-ADV-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 6/4/08 for Jhn19:31:1
[ADV-V-ADV-S-ADV-ADV] ADV V* ADV S ADV ADV -> CL
	? { "ADV-V-ADV-S-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-V-ADV-ADV-ADV] ADV V* ADV ADV ADV -> CL
	? { ( ! ( "ADV-V-PP-PP-PP" in 0.BR || "ADV-V-ADV-ADV-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-PP-PP-PP" in 0.AR || "ADV-V-ADV-ADV-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 5/6/09 mat10:18:1
[ADV-V-ADV-ADV-IO] ADV V* ADV ADV IO -> CL
	? { "ADV-V-ADV-ADV-IO" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-V-S] ADV V* S -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( "ADV-V-S" in 0.BR || "PP-V-S" in 0.BR )
	    && ! ( (2.End-0.Start) in 0.NB ) 
        && ! ( 1.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
	    )
	  || ( ( "ADV-V-S" in 0.AR || "PP-V-S" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB ) 
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-V-S-O] ADV V* S O -> CL
	? { ( "ADV-V-S-O" in 0.AR || "PP-V-S-O" in 0.AR ) 
	  && ! ( (3.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-S-O-ADV] ADV V* S O ADV -> CL
	? { "ADV-V-S-O-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-S-O-ADV-ADV] ADV V* S O ADV ADV -> CL
	? { "ADV-V-S-O-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	  
[ADV-ADV-V-S] ADV ADV V* S -> CL
	? { ( 2.Number == 3.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "ADV-ADV-V-S" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB ) 
        && ! ( 2.Unicode in {"ἰδοὺ","Ἰδοὺ","ἴδε","Ἴδε"} )
	    )
	  || ( "ADV-ADV-V-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-S-O] ADV ADV V* S O -> CL
	? { "ADV-ADV-V-S-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-ADV-V-S-O-ADV] ADV ADV V* S O ADV -> CL
	? { "ADV-ADV-V-S-O-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-S-ADV] ADV ADV V* S ADV -> CL
	? { "ADV-ADV-V-S-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
  
[ADV-ADV-ADV-V-S-ADV] ADV ADV ADV V* S ADV -> CL
	? { "ADV-ADV-ADV-V-S-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 7/13/11 luk3:1
[ADV-ADV-ADV-V-S-ADV-ADV] ADV ADV ADV V* S ADV ADV -> CL
	? { "ADV-ADV-ADV-V-S-ADV-ADV" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-ADV-ADV-ADV-V-S-ADV] ADV ADV ADV ADV V* S ADV -> CL
	? { "ADV-ADV-ADV-ADV-V-S-ADV" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 3/23/15 php1:18:17-1:20:30   
[ADV-ADV-ADV-ADV-V-S-ADV-ADV] ADV ADV ADV ADV V* S ADV ADV -> CL
	? { "ADV-ADV-ADV-ADV-V-S-ADV-ADV" in 0.AR && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 7.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-S-ADV-ADV] ADV ADV V* S ADV ADV -> CL
	? { "ADV-ADV-V-S-ADV-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-S-ADV-ADV-ADV] ADV ADV V* S ADV ADV ADV -> CL
	? { "ADV-ADV-V-S-ADV-ADV-ADV" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-V-S-ADV-ADV] ADV V* S ADV ADV -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( "ADV-V-S-ADV-ADV" in 0.BR || "PP-V-S-PP-ADV" in 0.BR || "PP-V-S-PP-PP" in 0.BR )
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-S-ADV-ADV" in 0.AR || "PP-V-S-PP-ADV" in 0.AR || "PP-V-S-PP-PP" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-S-ADV-ADV-ADV] ADV V* S ADV ADV ADV -> CL
	? { "ADV-V-S-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[ADV-V-S-ADV-ADV-ADV-ADV] ADV V* S ADV ADV ADV ADV -> CL
	? { "ADV-V-S-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	  
[V-O] V* O -> CL
	? { ( 0.Punc == ""
	    && ! ( "V-O" in 0.BR ) 
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "V2Adjp" in 0.AR )
        && ! ( "V-S" in 0.AR && ( "CL2S" in 1.AR || ( "np2S" in 1.AR || "Np2S" in 1.AR ) ) )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} 
              && ! ( 1.Unicode in {"χεῖράς","χρηστότητα"} )
              )
	    )
	  || ( "V-O" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 1.Punc; } 
	
[V-O-IO] V* O IO -> CL
	? { ( ! ( "V-O-IO" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
	    )
	  || ( "V-O-IO" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; }
	
[V-O-IO-O2] V* O IO O2 -> CL
	? { "V-O-IO-O2" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-O-IO] V* ADV O IO -> CL
	? { "V-ADV-O-IO" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; } 
	
[V-O-IO-ADV] V* O IO ADV -> CL
	? { ( ! ( "V-O-IO-PP" in 0.BR || "V-O-IO-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-O-IO-PP" in 0.AR || "V-O-IO-ADV" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 3.Punc; }
	
[V-O-IO-ADV-ADV] V* O IO ADV ADV -> CL
	? { "V-O-IO-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

[O-IO-ADV-V] O IO ADV V* -> CL
	? { "O-IO-ADV-V" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

//Randall 8/29/08 for act22:5:11
[V-ADV-O-O2] V* ADV O O2 -> CL
	? { "V-ADV-O-O2" in 0.AR  
	  && ! ( (3.End-0.Start) in 0.NB ) 	 
	  }
	>> { .Punc = 3.Punc; } 

//Randall 5/9/09 mrk3:16:1-3:17:20
[V-IO-O-O2] V* IO O O2 -> CL
	? { "V-IO-O-O2" in 0.AR  
	  && ! ( (3.End-0.Start) in 0.NB ) 	 
	  }
	>> { .Punc = 3.Punc; } 
	  
[V-O-O2] V* O O2 -> CL
	? { ( ! ( "V-O-O2" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "V-O-O2" in 0.AR  && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; } 
	
[V-O-O2-ADV] V* O O2 ADV -> CL
	? { "V-O-O2-ADV" in 0.AR  
	  && ! ( (3.End-0.Start) in 0.NB ) 	 
	  }
	>> { .Punc = 3.Punc; } 

//Randall 7/29/08 Rom 6:12:1
[V-O-O2-IO] V* O O2 IO -> CL
	? { "V-O-O2-IO" in 0.AR  
	  && ! ( (3.End-0.Start) in 0.NB ) 	 
	  }
	>> { .Punc = 3.Punc; } 

//Randall 7/29/08 Rom 6:19:9
[V-O-O2-IO-ADV] V* O O2 IO ADV -> CL
	? { "V-O-O2-IO-ADV" in 0.AR  
	  && ! ( (4.End-0.Start) in 0.NB ) 	 
	  }
	>> { .Punc = 4.Punc; }

[V-O-O2-ADV-ADV] V* O O2 ADV ADV -> CL
	? { "V-O-O2-ADV-ADV" in 0.AR  
	  && ! ( (4.End-0.Start) in 0.NB ) 	 
	  }
	>> { .Punc = 4.Punc; } 

//Randall 9/13/08 1Th3:12:1
[V-O-O2-ADV-ADV-ADV] V* O O2 ADV ADV ADV -> CL
	? { "V-O-O2-ADV-ADV-ADV" in 0.AR  
	  && ! ( (5.End-0.Start) in 0.NB ) 	 
	  }
	>> { .Punc = 5.Punc; } 

//Randall 7/29/08 Rom 6:16:1
[IO-V-O-O2-ADV] IO V* O O2 ADV -> CL
	? { "IO-V-O-O2-ADV" in 0.AR  
	  && ! ( (4.End-0.Start) in 0.NB ) 	 
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		.Punc = 4.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 8/2/09 act1:3:1 (new)
[IO-ADV-V-O-O2-ADV-ADV-ADV] IO ADV V* O O2 ADV ADV ADV -> CL
	? { "IO-ADV-V-O-O2-ADV-ADV-ADV" in 0.AR  
	  && (7.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		.Punc = 6.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 3/30/09 act1:3:1 (old)
[IO-V-O-O2-ADV-ADV-ADV] IO V* O O2 ADV ADV ADV -> CL
	? { "IO-V-O-O2-ADV-ADV-ADV" in 0.AR  
	  && ! ( (6.End-0.Start) in 0.NB ) 	 
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		.Punc = 6.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[O-O2-V] O O2 V* -> CL
	? { "O-O2-V" in 0.AR  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		.Punc = 2.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 5/22/09 2co5:21:1-5:21:15
[O-ADV-O2-V] O ADV O2 V* -> CL
	? { "O-ADV-O2-V" in 0.AR  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		.Punc = 3.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

//Randall 11/16/08 for Jhn7:23:1-7:23:21
[O-O2-V-ADV] O O2 V* ADV -> CL
	? { "O-O2-V-ADV" in 0.AR  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		.Punc = 3.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 6/17/09 2th3:9:1
[O-O2-V-IO-ADV] O O2 V* IO ADV -> CL
	? { "O-O2-V-IO-ADV" in 0.AR  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		.Punc = 4.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[O2-O-V] O2 O V* -> CL
	? { "O2-O-V" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 2.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 12/24/08 Mat26:71:1 40026073016
[S-O2-O-V] S O2 O V* -> CL
	? { "S-O2-O-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

//Randall 2/17/09 Jms1:27:1
[O2-O-V-ADV] O2 O V* ADV -> CL
	? { "O2-O-V-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

//Randall 10/24/08 1Tm1:12:1
[O2-O-V-ADV-ADV] O2 O V* ADV ADV -> CL
	? { "O2-O-V-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[ADV-O2-O-V] ADV O2 O V* -> CL
	? { "ADV-O2-O-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 5/23/09 2co11:9:20-11:9:28
[ADV-O2-O-IO-V] ADV O2 O IO V* -> CL
	? { "ADV-O2-O-IO-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 4/19/09 act14:17:1-14:17:20
[ADV-O2-O-V-ADV] ADV O2 O V* ADV -> CL
	? { "ADV-O2-O-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 6/15/09 php3:8:1-3:11:9
[V-O2] V* O2 -> CL
	? { "V-O2" in 0.AR 
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; }

// Randall 3/26/15 for N1904 2pe2:4:1-2:10:12
[O2-V] O2 V* -> CL
	? { "O2-V" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; }

// Randall 3/26/15 for 2pe2:4:1-2:10:12
[O2-IO-V] O2 IO V* -> CL
	? { "O2-IO-V" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; }

// Randall 3/19/15 for jud1:24:1
[V-ADV-O2-ADV] V* ADV O2 ADV -> CL
	? { "V-ADV-O2-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }

// Randall 3/26/15 2pe1:8:1-1:8:19
[S-ADV-O2-V-ADV] S ADV O2 V* ADV -> CL
	? { "S-ADV-O2-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[V-O2-O] V* O2 O -> CL
	? { "V-O2-O" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; }

//Randall 9/13/08 for 2Pe1:19:1
[V-O2-O-ADV] V* O2 O ADV -> CL
	? { "V-O2-O-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }

[V-O-ADV-S] V* O ADV S -> CL
	? { "V-O-ADV-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }

// Randall 5-31-08 for Luk3:14:1
[V-O-ADV-S-ADV] V* O ADV S ADV -> CL
	? { "V-O-ADV-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }

	  
[V-O-ADV-ADV-S-ADV] V* O ADV ADV S ADV -> CL
	? { "V-O-ADV-ADV-S-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }
		  
[V-O-ADV-IO] V* O ADV IO -> CL
	? { "V-O-ADV-IO" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	  
[V-O-ADV-IO-ADV] V* O ADV IO ADV -> CL
	? { "V-O-ADV-IO-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	  
[ADV-V-O-ADV-IO-ADV] ADV V* O ADV IO ADV -> CL
	? { "ADV-V-O-ADV-IO-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	  
[V-O-ADV-O2] V* O ADV O2 -> CL
	? {  ( "V-O-PP-O2" in 0.AR || "V-O-ADV-O2" in 0.AR )
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	  
[V-O-ADV-ADV] V* O ADV ADV -> CL
	? { ( "V-O-PP-ADV" in 0.AR || "V-O-ADV-ADV" in 0.AR || "V-O-PP-PP" in 0.AR )
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-O-ADV-ADV-ADV] V* O ADV ADV ADV -> CL
	? { ( ! ( "V-O-PP-PP-PP" in 0.BR || "V-O-ADV-ADV-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-O-PP-PP-PP" in 0.AR || "V-O-ADV-ADV-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 4.Punc; } 
	
[V-O-ADV-ADV-ADV-ADV] V* O ADV ADV ADV ADV -> CL
	? { "V-O-ADV-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }

[V-O-ADV-IO-ADV-ADV] V* O ADV IO ADV ADV -> CL
	? { "V-O-ADV-IO-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }

//Andi 020508	
[V-O-ADV-ADV-ADV-ADV-ADV] V* O ADV ADV ADV ADV ADV -> CL
	? { "V-O-ADV-ADV-ADV-ADV-ADV" in 0.AR
	  && (6.End-0.Start) in 0.RB
	  }
	>> { .Punc = 6.Punc; }
	
[V-O-ADV] V* O ADV -> CL
	? { ( ! ( "V-O-ADV" in 0.BR || "V-O-PP" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-O-ADV" in 0.AR || "V-O-PP" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; } 
	
[V-ADV] V* ADV -> CL
	? { ( ! ( "V-ADV" in 0.BR || "V-PP" in 0.BR ) 
	    && 0.Punc==""
	    && ! ( (1.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 0.UnicodeLemma in {"λέγω"} && 1.Rule in {"CL2ADV"} && 1.Case in {"Nominative"} )
	    )
	  || ( ( "V-ADV" in 0.AR || "V-PP" in 0.AR )
	     && ! ( (1.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 1.Punc; } 

[V-ADV-ADV] V* ADV ADV -> CL
	? { ( 0.Punc=="" && 1.Punc==""
	    && ! ( "V-ADV-ADV" in 0.BR || "V-PP-PP" in 0.BR || "V-PP-ADV" in 0.BR || "V-ADV-PP" in 0.BR )
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
	    )
	  || ( ( "V-ADV-ADV" in 0.AR || "V-PP-PP" in 0.AR || "V-PP-ADV" in 0.AR || "V-ADV-PP" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; }
		
[V-ADV-S] V* ADV S -> CL
	? { ( ! ( "V-ADV-S" in 0.BR || "V-PP-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 0.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
        && ! ( 0.Mood in {"Participle"} && 0.UnicodeLemma in {"λέγω"} && 2.Case in {"Nominative"} )
	    )
	  || ( ( "V-ADV-S" in 0.AR || "V-PP-S" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; } 
	
[V-ADV-S-ADV-ADV] V* ADV S ADV ADV -> CL
	? { ( ! ( "V-ADV-S-PP-PP" in 0.BR || "V-ADV-S-ADV-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-ADV-S-PP-PP" in 0.AR || "V-ADV-S-ADV-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 4.Punc; } 
	
[V-ADV-ADV-S] V* ADV ADV S -> CL
	? { "V-ADV-ADV-S" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-ADV-S-ADV] V* ADV ADV S ADV -> CL
	? { "V-ADV-ADV-S-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[ADV-V-ADV-ADV-S] ADV V* ADV ADV S -> CL
	? { "ADV-V-ADV-ADV-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[V-ADV-ADV-ADV-S-ADV] V* ADV ADV ADV S ADV -> CL
	? { "V-ADV-ADV-ADV-S-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }

// Randall 4/1/09 act4:27:1
[V-ADV-ADV-ADV-S-ADV-ADV] V* ADV ADV ADV S ADV ADV -> CL
	? { "V-ADV-ADV-ADV-S-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 6.Punc; }

[V-ADV-IO-ADV-S-ADV] V* ADV IO ADV S ADV -> CL
	? { "V-ADV-IO-ADV-S-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }
	
[V-ADV-ADV-ADV-S] V* ADV ADV ADV S -> CL
	? { "V-ADV-ADV-ADV-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-O] V* ADV O -> CL
	? { ( ! ( "V-ADV-O" in 0.BR || "V-PP-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-ADV-O" in 0.AR || "V-PP-O" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; } 
	
[V-ADV-IO] V* ADV IO -> CL
	? { ( ! ( "V-ADV-IO" in 0.BR || "V-PP-IO" in 0.BR) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-ADV-IO" in 0.AR || "V-PP-IO" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; }
	
[V-ADV-ADV-IO] V* ADV ADV IO -> CL
	? { "V-ADV-ADV-IO" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-ADV-ADV-IO] V* ADV ADV ADV IO -> CL
	? { "V-ADV-ADV-ADV-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-IO-S] V* ADV IO S -> CL
	? { "V-ADV-IO-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-IO-O] V* ADV IO O -> CL
	? { "V-ADV-IO-O" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-ADV-IO-ADV-O] V* ADV IO ADV O -> CL
	? { "V-ADV-IO-ADV-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-ADV-IO-ADV] V* ADV IO ADV -> CL
	? { ( ! ( "V-ADV-IO-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "V-ADV-IO-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 3.Punc; }
	
[ADV-V-ADV-IO-ADV] ADV V* ADV IO ADV -> CL
	? { "ADV-V-ADV-IO-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[ADV-V-O] ADV V* O -> CL
	? { ( ! ( "ADV-V-O" in 0.BR || "PP-V-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-O" in 0.AR || "PP-V-O" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-O-ADV-S] ADV V* O ADV S -> CL
	? { ( ! ( "ADV-V-O-PP-S" in 0.BR || "ADV-V-O-ADV-S" in 0.BR ) 
	    && 1.Number == 4.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-O-PP-S" in 0.AR || "ADV-V-O-ADV-S" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-O-ADV-S-ADV-ADV] ADV V* O ADV S ADV ADV -> CL
	? { "ADV-V-O-ADV-S-ADV-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
 
[ADV-V-O-ADV] ADV V* O ADV -> CL
	? { ( "ADV-V-O-ADV" in 0.AR || "PP-V-O-PP" in 0.AR || "ADV-V-O-PP" in 0.AR )
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }	   
	   
[ADV-V-O-S] ADV V* O S -> CL
	? { ( ! ( "ADV-V-O-S" in 0.BR || "PP-V-O-S" in 0.BR ) 
	    && 1.Number == 3.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-O-S" in 0.AR || "PP-V-O-S" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-O-S] ADV ADV V* O S -> CL
	? { "ADV-ADV-V-O-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-O-S-ADV] ADV V* O S ADV -> CL
	? { ( ! ( "ADV-V-O-S-PP" in 0.BR || "ADV-V-O-S-ADV" in 0.BR ) 
	    && 1.Number == 3.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-V-O-S-PP" in 0.AR || "ADV-V-O-S-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 1/9/09 Heb10:2:1 ADV-V-ADV-O-S-ADV 
[ADV-V-ADV-O-S-ADV] ADV V* ADV O S ADV -> CL
	? { "ADV-V-ADV-O-S-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-O-S-ADV-ADV] ADV V* O S ADV ADV -> CL
	? { "ADV-V-O-S-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-V-O-S-O2] ADV V* O S O2 -> CL
	? { "ADV-V-O-S-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-O] ADV ADV V* O -> CL
	? { ( ! ( "ADV-ADV-V-O" in 0.BR || "PP-PP-V-O" in 0.BR || "PP-ADV-V-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-ADV-V-O" in 0.AR || "PP-PP-V-O" in 0.AR || "PP-ADV-V-O" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-O-ADV] ADV ADV V* O ADV -> CL
	? { "ADV-ADV-V-O-ADV" in 0.AR 
	     && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-O-ADV-ADV] ADV ADV V* O ADV ADV -> CL
	? { "ADV-ADV-V-O-ADV-ADV" in 0.AR 
	     && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-O-ADV-ADV-ADV] ADV ADV V* O ADV ADV ADV -> CL
	? { "ADV-ADV-V-O-ADV-ADV-ADV" in 0.AR 
	     && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 7/13/11 luk5:19
[ADV-ADV-ADV-V-O-ADV-ADV-ADV] ADV ADV ADV V* O ADV ADV ADV -> CL
	? { "ADV-ADV-ADV-V-O-ADV-ADV-ADV" in 0.AR 
	     && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 7.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 1/29/14 for N1904 mat13:29
[ADV-ADV-ADV-V-ADV-O] ADV ADV ADV V* ADV O -> CL
	? { "ADV-ADV-ADV-V-ADV-O" in 0.AR 
	     && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-V-O-ADV] ADV ADV ADV V* O ADV -> CL
	? { "ADV-ADV-ADV-V-O-ADV" in 0.AR 
	     && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
// ?
[ADV-ADV-V-IO] ADV ADV V* IO -> CL
	? { ( ! ( "ADV-ADV-V-IO" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-ADV-V-IO" in 0.AR
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-V-IO] ADV ADV ADV V* IO -> CL
	? { "ADV-ADV-ADV-V-IO" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-IO-S] ADV ADV V* IO S -> CL
	? { "ADV-ADV-V-IO-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-ADV-V-IO-ADV] ADV ADV V* IO ADV -> CL
	? { "ADV-ADV-V-IO-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-ADV-V-IO-ADV-ADV] ADV ADV V* IO ADV ADV -> CL
	? { "ADV-ADV-V-IO-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-V-IO-ADV-S] ADV ADV V* IO ADV S -> CL
	? { "ADV-ADV-V-IO-ADV-S" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-ADV-S-V] ADV ADV S V* -> CL
	? { "ADV-ADV-S-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-S-V] ADV ADV ADV S V* -> CL
	? { "ADV-ADV-ADV-S-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-S-V-ADV] ADV ADV ADV S V* ADV -> CL
	? { "ADV-ADV-ADV-S-V-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-V-ADV] ADV ADV S V* ADV -> CL
	? { "ADV-ADV-S-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-V-ADV-ADV] ADV ADV S V* ADV ADV -> CL
	? { "ADV-ADV-S-V-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-ADV-S-V-O] ADV ADV S V* O -> CL
	? { "ADV-ADV-S-V-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-ADV-V-O] ADV ADV S ADV ADV V* O -> CL
	? { "ADV-ADV-S-ADV-ADV-V-O" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-S-V-O] ADV ADV ADV S V* O -> CL
	? { "ADV-ADV-ADV-S-V-O" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 9/19/09 mrk15:1:1-15:1:23
[ADV-ADV-ADV-S-ADV-ADV-V] ADV ADV ADV S ADV ADV V* -> CL
	? { "ADV-ADV-ADV-S-ADV-ADV-V" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   	   
[ADV-ADV-S-V-O-ADV] ADV ADV S V* O ADV -> CL
	? { "ADV-ADV-S-V-O-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-IO-V] ADV ADV IO V* -> CL
	? { "ADV-ADV-IO-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-IO-V-ADV] ADV ADV IO V* ADV -> CL
	? { "ADV-ADV-IO-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-IO-V-O] ADV ADV IO V* O -> CL
	? { "ADV-ADV-IO-V-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-V-IO] ADV ADV S V* IO -> CL
	? { "ADV-ADV-S-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-ADV-S-V-IO] ADV ADV ADV S V* IO -> CL
	? { "ADV-ADV-ADV-S-V-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-V-IO-ADV] ADV ADV S V* IO ADV -> CL
	? { "ADV-ADV-S-V-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-V] ADV ADV S ADV V* -> CL
	? { ( ! ( "PP-ADV-S-PP-V" in 0.BR || "ADV-ADV-S-ADV-V" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "PP-ADV-S-PP-V" in 0.AR || "ADV-ADV-S-ADV-V" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-V-O] ADV ADV S ADV V* O -> CL
	? { "ADV-ADV-S-ADV-V-O" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 4/2/09 act5:26:1
[ADV-ADV-S-ADV-V-O-ADV] ADV ADV S ADV V* O ADV -> CL
	? { "ADV-ADV-S-ADV-V-O-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-V-ADV] ADV ADV S ADV V* ADV -> CL
	? { "ADV-ADV-S-ADV-V-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-V-ADV-ADV] ADV ADV S ADV V* ADV ADV -> CL
	? { "ADV-ADV-S-ADV-V-ADV-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-V-ADV-ADV-ADV] ADV ADV S ADV V* ADV ADV ADV -> CL
     ? { "ADV-ADV-S-ADV-V-ADV-ADV-ADV" in 0.AR
       && ! ( (7.End-0.Start) in 0.NB )
       }
     >> { .RB = 0.RB; .NB = 0.NB; 
          .Punc = 7.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[V-IO] V* IO -> CL
	? { ( ! ( "V-IO" in 0.BR ) 
	    && 0.Punc==""
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "V2Adjp" in 0.AR )
	    )
	  || ( "V-IO" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 1.Punc; } 
	
[V-IO-S] V* IO S -> CL
	? { ( ! ( "V-IO-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 2.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 2.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 2.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 2.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 2.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( "V-IO-S" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; } 
	
[V-IO-S-O] V* IO S O -> CL
	? { ( ! ( "V-IO-S-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 2.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 2.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 2.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 2.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 2.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( "V-IO-S-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 3.Punc; }
	
[V-IO-S-O-ADV] V* IO S O ADV -> CL
	? { "V-IO-S-O-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB ) 
	  }
	>> { .Punc = 4.Punc; }

// ?
[V-IO-ADV] V* IO ADV -> CL
	? { ( ! ( "V-IO-ADV" in 0.BR || "V-IO-PP" in 0.BR) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-IO-ADV" in 0.AR || "V-IO-PP" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; }	
	
[V-IO-ADV-S] V* IO ADV S -> CL
	? { "V-IO-ADV-S" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-IO-ADV-ADV] V* IO ADV ADV -> CL
	? { "V-IO-ADV-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }	
	
[V-IO-ADV-ADV-ADV] V* IO ADV ADV ADV -> CL
	? { "V-IO-ADV-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }


//Randall 8/30/08 for act22:6:1
[V-IO-ADV-ADV-S] V* IO ADV ADV S -> CL
	? { "V-IO-ADV-ADV-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-IO-ADV-ADV-ADV-ADV] V* IO ADV ADV ADV ADV -> CL
	? { "V-IO-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; }
	
[V-IO-ADV-ADV-ADV-ADV-ADV] V* IO ADV ADV ADV ADV ADV -> CL
	? { "V-IO-ADV-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 6.Punc; }
	
[V-IO-ADV-ADV-O] V* IO ADV ADV O -> CL
	? { "V-IO-ADV-ADV-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-IO-ADV-S-ADV] V* IO ADV S ADV -> CL
	? { ( ! ( "V-IO-ADV-S-PP" in 0.BR || "V-IO-ADV-S-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "V-IO-ADV-S-PP" in 0.AR || "V-IO-ADV-S-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 4.Punc; }
	
[V-IO-S-ADV] V* IO S ADV -> CL
	? { ( ! ( "V-IO-S-ADV" in 0.BR || "V-IO-S-PP" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
		&& ! ( 0.Person=="First"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 2.UnicodeLemma in {"ἐγώ","ἐγώ"}
				  && 2.Case=="Nominative"
				  )
			 )
		&& ! ( 0.Person=="Second"
             && 0.UnicodeLemma in {"λέγω"}
			 && ! ( 2.UnicodeLemma in {"σύ","σύ","σύ"}
				  && 2.Case=="Nominative"
				  )
             )
        && ! ( 0.Person=="Third"
             && 0.UnicodeLemma in {"λέγω"}
             && 2.UnicodeLemma in {"ἐγώ","ἐγώ","σύ","σύ","σύ"}
             )
	    )
	  || ( ( "V-IO-S-ADV" in 0.AR || "V-IO-S-PP" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 3.Punc; } 
	
[V-IO-S-ADV-ADV] V* IO S ADV ADV -> CL
	? { "V-IO-S-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; } 
	
[V-IO-S-ADV-ADV-ADV] V* IO S ADV ADV ADV -> CL
	? { "V-IO-S-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; } 
	
[V-IO-O] V* IO O -> CL
	? { ( ! ( "V-IO-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "V-IO-O" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; }

[V-IO-O-ADV] V* IO O ADV -> CL
	? { "V-IO-O-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; }
	
[V-IO-O-ADV-ADV] V* IO O ADV ADV -> CL
	? { "V-IO-O-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; }
	
[V-IO-O-S] V* IO O S -> CL
	? { ( ! ( "V-IO-O-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "V-IO-O-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 3.Punc; }
	
[V-IO-ADV-O] V* IO ADV O -> CL
	? { ( ! ( "V-IO-PP-O" in 0.BR || "V-IO-ADV-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
		&& ! ( 2.Lemma in {"kai/"} )
		&& ! ( 2.UnicodeLemma in {"καί","καί"} )
	    )
	  || ( ( "V-IO-PP-O" in 0.AR || "V-IO-ADV-O" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 3.Punc; }  
	  
[S-V] S V* -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc == ""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V" in 0.BR ) 
	    && ! ( (1.End-0.Start) in 0.NB )
		&& ! ( 1.Person=="First"
			 && ! ( 0.UnicodeLemma in {"ἐγώ","αὐτός","πᾶς","ἐγώ","αὐτός","πᾶς"} // addresses JT & SBL morph font differences
				  && 0.Case=="Nominative"
				  )
			 )
		&& ! ( 1.Person=="Second"
			 && ! ( 0.UnicodeLemma in {"σύ","αὐτός","πᾶς","σύ","σύ","αὐτός","πᾶς"}
				  && 0.Case=="Nominative"
				  )
			 )
	    )
	  || ( "S-V" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 1.Punc;
	   }

[S-V-ADV-O] S V* ADV O -> CL
	? { "S-V-ADV-O" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-ADV-ADV-O] S V* ADV ADV O -> CL
	? { "S-V-ADV-ADV-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-V-ADV-O-ADV] S V* ADV O ADV -> CL
	? { "S-V-ADV-O-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-V-ADV-O-IO] S V* ADV O IO -> CL
	? { "S-V-ADV-O-IO" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 9/8/09 2co5:18:1-5:19:23
[S-V-ADV-O-IO-ADV] S V* ADV O IO ADV -> CL
	? { "S-V-ADV-O-IO-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-V-ADV-IO] S V* ADV IO -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc == "" && 1.Punc=="" && 2.Punc==""
	    && ! ( "S-V-PP-IO" in 0.BR || "S-V-ADV-IO" in 0.BR ) 
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-PP-IO" in 0.AR || "S-V-ADV-IO" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-ADV-IO-ADV] S V* ADV IO ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc == "" && 1.Punc=="" && 2.Punc==""
	    && ! ( "S-V-PP-IO-PP" in 0.BR || "S-V-ADV-IO-ADV" in 0.BR || "S-V-PP-IO-ADV" in 0.BR ) 
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-PP-IO-PP" in 0.AR || "S-V-ADV-IO-ADV" in 0.AR || "S-V-PP-IO-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 5/6/09 mat2:19:1-2:20:22 
[S-V-ADV-IO-ADV-ADV] S V* ADV IO ADV ADV -> CL
	? { "S-V-ADV-IO-ADV-ADV" in 0.AR
	  && (5.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-V-ADV-IO-ADV] ADV S V* ADV IO ADV -> CL
	? { "ADV-S-V-ADV-IO-ADV" in 0.AR
	  && (5.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-V-ADV-IO-ADV-ADV] ADV S V* ADV IO ADV ADV -> CL
	? { "ADV-S-V-ADV-IO-ADV-ADV" in 0.AR
	  && (6.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[S-V-ADV-ADV] S V* ADV ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc == "" && 1.Punc=="" && 2.Punc==""
	    && ! ( "S-V-PP-PP" in 0.BR || "S-V-ADV-ADV" in 0.BR || "S-V-ADV-PP" in 0.BR ) 
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-PP-PP" in 0.AR || "S-V-ADV-ADV" in 0.AR || "S-V-ADV-PP" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-ADV-ADV-ADV] S V* ADV ADV ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc == "" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( "S-V-PP-PP-ADV" in 0.BR || "S-V-ADV-ADV-ADV" in 0.BR ) 
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-PP-PP-ADV" in 0.AR || "S-V-ADV-ADV-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-V-ADV-ADV-ADV-ADV] S V* ADV ADV ADV ADV -> CL
	? { "S-V-ADV-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

//Randall 8/25/08 for Acts 10:1:1
[S-V-ADV-ADV-ADV-O] S V* ADV ADV ADV O -> CL
	? { "S-V-ADV-ADV-ADV-O" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-V-ADV] S V* ADV -> CL
	? { ( 0.Number == 1.Number
	    && 0.Punc == "" && 1.Punc==""
	    && ! ( "S-V-ADV" in 0.BR || "S-V-PP" in 0.BR ) 
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( (2.End-0.Start) in 0.NB )
		&& ! ( 1.Person=="First"
			 && ! ( 0.UnicodeLemma in {"ἐγώ","αὐτός","πᾶς","ἐγώ","αὐτός","πᾶς"}
				  && 0.Case=="Nominative"
				  )
			 )
		&& ! ( 1.Person=="Second"
			 && ! ( 0.UnicodeLemma in {"σύ","αὐτός","πᾶς","σύ","σύ","αὐτός","πᾶς"}
				  && 0.Case=="Nominative" 
				  )
			 )
	    )
	  || ( ( "S-V-ADV" in 0.AR || "S-V-PP" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[ADV-S-V] ADV S V* -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
        && ! ( 2.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
	    && ! ( "ADV-S-V" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-S-V" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
		 .Punc = 2.Punc;
	   }
	   
[ADV-S-V-O] ADV S V* O -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-V-O" in 0.BR || "PP-S-V-O" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-S-V-O" in 0.AR || "PP-S-V-O" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-S-V-O-IO] ADV S V* O IO -> CL
	? { "ADV-S-V-O-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 4/28/09 act24:27:10-24:27:21
[ADV-S-V-O-O2] ADV S V* O O2 -> CL
	? { "ADV-S-V-O-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-S-V-O-IO-ADV] ADV S V* O IO ADV -> CL
	? { "ADV-S-V-O-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-V-ADV] ADV S V* ADV -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-V-PP" in 0.BR || "ADV-S-V-ADV" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-S-V-PP" in 0.AR || "ADV-S-V-ADV" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-S-V-ADV-O] ADV S V* ADV O -> CL
	? { "ADV-S-V-ADV-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-S-V-ADV-O-ADV] ADV S V* ADV O ADV -> CL
	? { "ADV-S-V-ADV-O-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { 
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-S-V-ADV-ADV] ADV S V* ADV ADV -> CL
	? { "ADV-S-V-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { 
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-S-V-ADV-ADV-ADV] ADV S V* ADV ADV ADV -> CL
	? { "ADV-S-V-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { 
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-V-ADV-ADV-ADV] ADV ADV S V* ADV ADV ADV -> CL
	? { "ADV-ADV-S-V-ADV-ADV-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { 
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-S-V-ADV-ADV-ADV-ADV] ADV S V* ADV ADV ADV ADV -> CL
	? { "ADV-S-V-ADV-ADV-ADV-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} ) // "that/which"
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[ADV-S-V-O-ADV] ADV S V* O ADV -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-V-O-PP" in 0.BR || "ADV-S-V-O-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-S-V-O-PP" in 0.AR || "ADV-S-V-O-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} ) // "that/which"
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 1/8/09 Heb9:13
[ADV-S-V-O-ADV-ADV] ADV S V* O ADV ADV -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc=="" && 4.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-V-O-ADV-ADV" in 0.BR ) 
	    && ! ( (5.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-S-V-O-ADV-ADV" in 0.AR
	     && ! ( (5.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-V-IO] ADV S V* IO -> CL
	? { ( 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-V-IO" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-S-V-IO" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-S-V-IO-ADV] ADV S V* IO ADV -> CL
	? { "ADV-S-V-IO-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-V-IO-ADV-O] S V* IO ADV O -> CL
	? { "S-V-IO-ADV-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-V-IO-ADV-ADV] S V* IO ADV ADV -> CL
	? { "S-V-IO-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	     
[ADV-S-V-IO-ADV-ADV] ADV S V* IO ADV ADV -> CL
	? { "ADV-S-V-IO-ADV-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-V-IO-O] ADV S V* IO O -> CL
	? { "ADV-S-V-IO-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-S-V-IO-O] ADV ADV S V* IO O -> CL
	? { "ADV-ADV-S-V-IO-O" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	
[ADV-S-O-V] ADV S O V* -> CL
	? { ( 1.Number == 3.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-O-V" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-S-O-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-S-O-O2-V] ADV S O O2 V* -> CL
	? { "ADV-S-O-O2-V" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 8/5/11 jhn4:23
[ADV-S-O-V-O2] ADV S O V* O2 -> CL
	? { "ADV-S-O-V-O2" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

//Randall 06/30/08 for Gal5:2:1
[S-O-O2-V] S O O2 V* -> CL
	? { "S-O-O2-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[S-O-O2-V-ADV] S O O2 V* ADV -> CL
	? { "S-O-O2-V-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-O-V] ADV ADV S O V* -> CL
	? { "ADV-ADV-S-O-V" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 9/23/09 1th4:14
[ADV-ADV-S-O-V-ADV] ADV ADV S O V* ADV -> CL
	? { "ADV-ADV-S-O-V-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	      
[ADV-S-O-V-ADV] ADV S O V* ADV -> CL
	? { ( 1.Number == 3.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-O-V-PP" in 0.BR || "ADV-S-O-V-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-S-O-V-PP" in 0.AR || "ADV-S-O-V-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

[ADV-S-ADV-V] ADV S ADV V* -> CL
	? { ( 1.Number == 3.Number
	    && 1.Punc=="" && 2.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
        && ! ( 3.Unicode in {"ἰδοὺ","Ἰδοὺ","ἰδού","ἴδε","Ἴδε"} )
	    && ! ( "ADV-S-ADV-V" in 0.BR || "ADV-S-PP-V" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-S-ADV-V" in 0.AR || "ADV-S-PP-V" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }
	   
[ADV-S-ADV-V-O] ADV S ADV V* O -> CL
	? { "ADV-S-ADV-V-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-S-ADV-V-O-ADV] ADV S ADV V* O ADV -> CL
	? { "ADV-S-ADV-V-O-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

//Randall 9/15/08 Co11:9:1
[ADV-S-ADV-ADV-V-O] ADV S ADV ADV V* O -> CL
	? { "ADV-S-ADV-ADV-V-O" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 9/23/10 2pe2:10:13
[ADV-S-ADV-ADV-V-ADV-O] ADV S ADV ADV V* ADV O -> CL
	? { "ADV-S-ADV-ADV-V-ADV-O" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

// Randall 8/24/09 2pe2:10:13-2:11:15
[ADV-S-ADV-ADV-V-ADV-ADV-O] ADV S ADV ADV V* ADV ADV O -> CL
	? { "ADV-S-ADV-ADV-V-ADV-ADV-O" in 0.AR 
	  && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
	   }

// Randall 8/20/09 jhn13:1:1
[ADV-S-ADV-ADV-ADV-V-O] ADV S ADV ADV ADV V* O -> CL
	? { "ADV-S-ADV-ADV-ADV-V-O" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[ADV-S-ADV-ADV-V-O-ADV] ADV S ADV ADV V* O ADV -> CL
	? { "ADV-S-ADV-ADV-V-O-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

// Randall 6/22/09 mat27:3:1
[ADV-S-ADV-ADV-V-O-IO-ADV] ADV S ADV ADV V* O IO ADV -> CL
	? { "ADV-S-ADV-ADV-V-O-IO-ADV" in 0.AR 
	  && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
	   }

// Randall 1/9/09 Heb9:27:1 ADV-S-ADV-ADV-V-IO-ADV
[ADV-S-ADV-ADV-ADV-V-IO-ADV] ADV S ADV ADV ADV V* IO ADV -> CL
	? { "ADV-S-ADV-ADV-ADV-V-IO-ADV" in 0.AR 
	  && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
	   }
	   
[ADV-S-ADV-V-O-ADV-ADV] ADV S ADV V* O ADV ADV -> CL
	? { "ADV-S-ADV-V-O-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-S-ADV-V-O-O2-ADV] ADV S ADV V* O O2 ADV -> CL
	? { "ADV-S-ADV-V-O-O2-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
   
[ADV-S-ADV-ADV-V] ADV S ADV ADV V* -> CL
	? { "ADV-S-ADV-ADV-V" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { 
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-ADV-V] ADV ADV S ADV ADV V* -> CL
	? { "ADV-ADV-S-ADV-ADV-V" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> {
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-ADV-V-ADV] ADV ADV S ADV ADV V* ADV -> CL
	? { "ADV-ADV-S-ADV-ADV-V-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> {
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-ADV-V-ADV-ADV] ADV ADV S ADV ADV V* ADV ADV -> CL
	? { "ADV-ADV-S-ADV-ADV-V-ADV-ADV" in 0.AR
	  && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> {
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 5/26/08 Eph1:13:1-1:14:15
[ADV-S-ADV-ADV-V-ADV-ADV-ADV] ADV S ADV ADV V* ADV ADV ADV -> CL
	? { "ADV-S-ADV-ADV-V-ADV-ADV-ADV" in 0.AR
	  && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> {
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   
[ADV-ADV-S-ADV-ADV-V-ADV-ADV-ADV] ADV ADV S ADV ADV V* ADV ADV ADV -> CL
	? { "ADV-ADV-S-ADV-ADV-V-ADV-ADV-ADV" in 0.AR
	  && ! ( (8.End-0.Start) in 0.NB )
	  }
	>> {
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 8.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

// Randall 9/3/11 acts20:1
[ADV-S-ADV-ADV-V-ADV] ADV S ADV ADV V* ADV -> CL
	? { "ADV-S-ADV-ADV-V-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> {
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }

//Randall 8/29/08 for act21:26:1
[ADV-S-ADV-ADV-V-ADV-ADV] ADV S ADV ADV V* ADV ADV -> CL
	? { "ADV-S-ADV-ADV-V-ADV-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> {
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   	   
[ADV-S-ADV-V-IO] ADV S ADV V* IO -> CL
	? { ( 1.Number == 3.Number
	    && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-ADV-V-IO" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-S-ADV-V-IO" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

//Randall 11/11/08 for Heb1:3:1	   
[ADV-S-ADV-V-IO-ADV] ADV S ADV V* IO ADV -> CL
	? { "ADV-S-ADV-V-IO-ADV" in 0.AR 
	  &&! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	   }
	   	   
[ADV-S-ADV-V-ADV-IO] ADV S ADV V* ADV IO -> CL
	? { "ADV-S-ADV-V-ADV-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-ADV-V-ADV-ADV] ADV S ADV V* ADV ADV -> CL
	? { "ADV-S-ADV-V-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

//Doris 7/02/08 for 1tm1:5:1-1:7:12
[ADV-S-ADV-V-ADV-ADV-ADV] ADV S ADV V* ADV ADV ADV -> CL
     ? { "ADV-S-ADV-V-ADV-ADV-ADV" in 0.AR
       && ! ( (6.End-0.Start) in 0.NB )
       }
     >> { .RB = 0.RB; .NB = 0.NB; 
          .Punc = 6.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
        }
	   
[ADV-S-ADV-V-ADV-IO-ADV] ADV S ADV V* ADV IO ADV -> CL
	? { "ADV-S-ADV-V-ADV-IO-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[ADV-S-ADV-V-ADV] ADV S ADV V* ADV -> CL
	? { ( 1.Number == 3.Number
	    && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-S-ADV-V-PP" in 0.BR || "ADV-S-ADV-V-ADV" in 0.BR || "PP-S-ADV-V-PP" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-S-ADV-V-PP" in 0.AR || "ADV-S-ADV-V-ADV" in 0.AR || "PP-S-ADV-V-PP" in 0.AR)
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
//		 if ( 1.Lemma in {"o(/s"} || 1.UnicodeLemma in {"ὅς"} )
//	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-V] ADV ADV V* -> CL
	? { ( 1.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-ADV-V" in 0.BR || "ADV-PP-V" in 0.BR || "PP-ADV-V" in 0.BR) 
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 2.Unicode in {"ἰδοὺ","Ἰδοὺ","ἴδε","Ἴδε"} )
	    )
	  || ( ( "ADV-ADV-V" in 0.AR || "ADV-PP-V" in 0.AR || "PP-ADV-V" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
		 .Punc = 2.Punc;
	   }
	   
[ADV-ADV-ADV-V] ADV ADV ADV V* -> CL
	? { "ADV-ADV-ADV-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc;
	   }
	   
[ADV-ADV-ADV-ADV-V] ADV ADV ADV ADV V* -> CL
	? { "ADV-ADV-ADV-ADV-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc;
	   }

// Randall 9/23/10 Heb9:26:9
[ADV-ADV-ADV-ADV-ADV-V] ADV ADV ADV ADV ADV V* -> CL
	? { "ADV-ADV-ADV-ADV-ADV-V" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
		 .Punc = 5.Punc;
	   }
	   
[ADV-ADV-ADV-V-O] ADV ADV ADV V* O -> CL
	? { "ADV-ADV-ADV-V-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-ADV-ADV-V-O] ADV ADV ADV ADV V* O -> CL
	? { "ADV-ADV-ADV-ADV-V-O" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-ADV-ADV-V-S] ADV ADV ADV V* S -> CL
	? { "ADV-ADV-ADV-V-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-ADV-V] S ADV V* -> CL
	? { ( 0.Punc=="" && 1.Punc=="" 
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-ADV-V" in 0.BR || "S-PP-V" in 0.BR )
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 2.Unicode in {"ἰδοὺ","Ἰδοὺ","ἴδε","Ἴδε"} )
	    )
	  || ( ( "S-ADV-V" in 0.AR || "S-PP-V" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[O-S-ADV-V] O S ADV V* -> CL
	? { "O-S-ADV-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-ADV-S-ADV-V] O ADV S ADV V* -> CL
	? { "O-ADV-S-ADV-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-S-ADV-V-ADV] O S ADV V* ADV -> CL
	? { "O-S-ADV-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	    
[O-S-ADV-V-IO] O S ADV V* IO -> CL
	? { "O-S-ADV-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-ADV-V] S ADV ADV V* -> CL
	? { "S-ADV-ADV-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-ADV-ADV-ADV-V] S ADV ADV ADV V* -> CL
	? { "S-ADV-ADV-ADV-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-ADV-ADV-ADV-V-ADV] S ADV ADV ADV V* ADV -> CL
	? { "S-ADV-ADV-ADV-V-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

//Randall 8/29/08 for act20:9:1
[S-ADV-ADV-ADV-V-ADV-ADV] S ADV ADV ADV V* ADV ADV -> CL
	? { "S-ADV-ADV-ADV-V-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[S-ADV-ADV-V-IO] S ADV ADV V* IO -> CL
	? { "S-ADV-ADV-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-ADV-ADV-V-IO-ADV] S ADV ADV V* IO ADV -> CL
	? { "S-ADV-ADV-V-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }


[S-ADV-ADV-V-ADV] S ADV ADV V* ADV -> CL
	? { "S-ADV-ADV-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-ADV-ADV-V-ADV-O] S ADV ADV V* ADV O -> CL
	? { "S-ADV-ADV-V-ADV-O" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-ADV-V-ADV-O-ADV] S ADV ADV V* ADV O ADV -> CL
	? { "S-ADV-ADV-V-ADV-O-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

// Randall 9/10/09 act9:1:1-9:2:24
[S-ADV-ADV-V-ADV-O-ADV-ADV] S ADV ADV V* ADV O ADV ADV -> CL
	? { "S-ADV-ADV-V-ADV-O-ADV-ADV" in 0.AR 
	  && ! ( (7.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
	   }
	      
[S-ADV-ADV-V-ADV-ADV-ADV] S ADV ADV V* ADV ADV ADV -> CL
	? { "S-ADV-ADV-V-ADV-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

// Randall 7/27/09 2pe2:10:13
[S-ADV-ADV-V-ADV-ADV-O] S ADV ADV V* ADV ADV O -> CL
	? { "S-ADV-ADV-V-ADV-ADV-O" in 0.AR 
	  && (6.End-0.Start) in 0.RB
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[S-ADV-ADV-V-ADV-ADV] S ADV ADV V* ADV ADV -> CL
	? { "S-ADV-ADV-V-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	     
[S-ADV-V-O] S ADV V* O -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc=="" 
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-ADV-V-O" in 0.BR || "S-PP-V-O" in 0.BR )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-ADV-V-O" in 0.AR || "S-PP-V-O" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-ADV-V-O-IO] S ADV V* O IO -> CL
	? { "S-ADV-V-O-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 3/11/09 jhn10:33:1 43010033016
[S-ADV-V-O-O2] S ADV V* O O2 -> CL
	? { "S-ADV-V-O-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-V-O-IO-ADV] S ADV V* O IO ADV -> CL
	? { "S-ADV-V-O-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-V-IO] S ADV V* IO -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc=="" 
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-ADV-V-IO" in 0.BR || "S-PP-V-IO" in 0.BR )
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-ADV-V-IO" in 0.AR || "S-PP-V-IO" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-ADV-ADV-V-O] S ADV ADV V* O -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-ADV-ADV-V-O" in 0.BR )
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( "S-ADV-ADV-V-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-ADV-V-O-ADV] S ADV ADV V* O ADV -> CL
	? { "S-ADV-ADV-V-O-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 8/1/11 luk23:11
[S-ADV-ADV-V-O-IO] S ADV ADV V* O IO -> CL
	? { "S-ADV-ADV-V-O-IO" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 6/22/09 mat27:24:1
[S-ADV-ADV-V-O-ADV-ADV] S ADV ADV V* O ADV ADV -> CL
	? { "S-ADV-ADV-V-O-ADV-ADV" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[S-ADV-ADV-ADV-V-O] S ADV ADV ADV V* O -> CL
	? { "S-ADV-ADV-ADV-V-O" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 5-18-08 for heb12:1:1-12:2:27  58012001002
[S-ADV-ADV-ADV-V-O-ADV] S ADV ADV ADV V* O ADV -> CL
	? { "S-ADV-ADV-ADV-V-O-ADV" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

// Randall 6-3-08 for Mrk14:30:1 41014030010
[S-ADV-ADV-ADV-ADV-O-V] S ADV ADV ADV ADV O V* -> CL
	? { "S-ADV-ADV-ADV-ADV-O-V" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[S-ADV-ADV-ADV-ADV-V-O] S ADV ADV ADV ADV V* O -> CL
	? { "S-ADV-ADV-ADV-ADV-V-O" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	  
[S-ADV-IO-V] S ADV IO V* -> CL
	? { "S-ADV-IO-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-ADV-IO-V-ADV] S ADV IO V* ADV -> CL
	? { "S-ADV-IO-V-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-IO-V-O] S ADV IO V* O -> CL
	? { "S-ADV-IO-V-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 2/6/09 luk9:21:1-9:22:25 42009021001
[S-ADV-IO-V-O-ADV] S ADV IO V* O ADV -> CL
	? { "S-ADV-IO-V-O-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-ADV-ADV-O-V] S ADV ADV ADV O V* -> CL
	? { "S-ADV-ADV-ADV-O-V" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 8/28/09 rom8:32:1
[S-ADV-ADV-ADV-O-IO-V] S ADV ADV ADV O IO V* -> CL
	? { "S-ADV-ADV-ADV-O-IO-V" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   	   
[S-ADV-V-O-ADV] S ADV V* O ADV -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc=="" 
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-ADV-V-O-ADV" in 0.BR || "S-ADV-V-O-PP" in 0.BR)
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-ADV-V-O-ADV" in 0.AR || "S-ADV-V-O-PP" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-V-O-ADV-ADV] S ADV V* O ADV ADV -> CL
	? { "S-ADV-V-O-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	  
[S-ADV-V-ADV] S ADV V* ADV -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc=="" 
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-ADV-V-ADV" in 0.BR || "S-ADV-V-PP" in 0.BR || "S-PP-V-ADV" in 0.BR || "S-PP-V-PP" in 0.BR )
	    && ! ( (3.End-0.Start) in 0.NB )
        && ! ( 2.UnicodeLemma in {"λέγω"} && 3.Rule in {"CL2ADV"} && 3.Case in {"Nominative"} )
	    )
	  || ( ( "S-ADV-V-ADV" in 0.AR || "S-ADV-V-PP" in 0.AR || "S-PP-V-ADV" in 0.AR || "S-PP-V-PP" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

[S-ADV-V-ADV-IO] S ADV V* ADV IO -> CL
	? { "S-ADV-V-ADV-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-V-ADV-O] S ADV V* ADV O -> CL
	? { "S-ADV-V-ADV-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 5-18-08 for heb7:27 (Inf CL made in apposition to "need")
[S-ADV-V-ADV-O-ADV] S ADV V* ADV O ADV -> CL
	? { "S-ADV-V-ADV-O-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 1-8-09 for heb7:27 (Inf CL not made in apposition to "need")
[S-ADV-V-ADV-O-ADV-ADV] S ADV V* ADV O ADV ADV -> CL
	? { "S-ADV-V-ADV-O-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[ADV-S-ADV-V-ADV-O] ADV S ADV V* ADV O -> CL
	? { "ADV-S-ADV-V-ADV-O" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-IO-V] S IO V* -> CL
	? { ( ! ( "S-IO-V" in 0.BR )
	    && 0.Number == 2.Number 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "S-IO-V" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[S-IO-ADV-V] S IO ADV V* -> CL
	? { "S-IO-ADV-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-IO-ADV-V-ADV] S IO ADV V* ADV -> CL
	? { "S-IO-ADV-V-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[ADV-S-IO-V] ADV S IO V* -> CL
	? { "ADV-S-IO-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }	   
[O-S-IO-V] O S IO V* -> CL
	? { "O-S-IO-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-O-S-IO-V] ADV O S IO V* -> CL
	? { "ADV-O-S-IO-V" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-O-S-IO-V] ADV ADV O S IO V* -> CL
	? { "ADV-ADV-O-S-IO-V" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	     
[S-IO-V-O] S IO V* O -> CL
	? { "S-IO-V-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-IO-V-ADV] S IO V* ADV -> CL
	? { "S-IO-V-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-IO-V-ADV-ADV] S IO V* ADV ADV -> CL
	? { "S-IO-V-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-IO-V-ADV-O] S IO V* ADV O -> CL
	? { "S-IO-V-ADV-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-S-IO-V-ADV-ADV] ADV ADV S IO V* ADV ADV -> CL
	? { "ADV-ADV-S-IO-V-ADV-ADV" in 0.AR && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[S-IO-V-O-ADV] S IO V* O ADV -> CL
	? { "S-IO-V-O-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-V-ADV-ADV] S ADV V* ADV ADV -> CL
	? { ( 0.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( "S-ADV-V-PP-ADV" in 0.BR || "S-ADV-V-ADV-ADV" in 0.BR || "S-PP-V-PP-ADV" in 0.BR) 
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-ADV-V-PP-ADV" in 0.AR || "S-ADV-V-ADV-ADV" in 0.AR || "S-PP-V-PP-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-V-ADV-ADV-ADV] S ADV V* ADV ADV ADV -> CL
	? { "S-ADV-V-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-V-ADV-ADV-ADV-ADV] S ADV V* ADV ADV ADV ADV -> CL
	? { "S-ADV-V-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-V-ADV-ADV-ADV-ADV] ADV V* ADV ADV ADV ADV -> CL
	? { "ADV-V-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 7/5/11 mrk10:29
[ADV-V-ADV-ADV-O-ADV] ADV V* ADV ADV O ADV -> CL
	? { "ADV-V-ADV-ADV-O-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-V-O] S V* O -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-O" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "S-V-O" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }

//Randall 12/17/08 for mat19:4:1-19:4:16 40019004008
[S-ADV-O2-V-O] S ADV O2 V* O -> CL
	? { "S-ADV-O2-V-O" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-O2-V-O] S O2 V* O -> CL
	? { "S-O2-V-O" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-O-IO] S V* O IO -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-O-IO" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "S-V-O-IO" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-O-IO-ADV] S V* O IO ADV -> CL
	? { "S-V-O-IO-ADV" in 0.AR 
	  && (4.End-0.Start) in 0.RB
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-O-O2] S V* O O2 -> CL
	? { "S-V-O-O2" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-O-O2-ADV] S V* O O2 ADV -> CL
	? { "S-V-O-O2-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-V-O-O2-ADV-ADV] S V* O O2 ADV ADV -> CL
	? { "S-V-O-O2-ADV-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

//Randall 12/26/08 for mrk3:12:1-3:12:9 41003012006
[ADV-O-O2-V] ADV O O2 V* -> CL
	? { "ADV-O-O2-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

//Randall 12/11/08 for Heb5:14:1
[ADV-O-O2-V-ADV] ADV O O2 V* ADV -> CL
	? { "ADV-O-O2-V-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 1/6/09 mrk10:6:1-10:6:9 41010006001
[ADV-O2-V-O] ADV O2 V* O -> CL
	? { "ADV-O2-V-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 4/29/09 2co12:20:1
[ADV-ADV-O2-V-O] ADV ADV O2 V* O -> CL
	? { "ADV-ADV-O2-V-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-V-O-O2] ADV V* O O2 -> CL
	? { "ADV-V-O-O2" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 9/4/11 act20:24
[ADV-V-O-O2-ADV] ADV V* O O2 ADV -> CL
	? { "ADV-V-O-O2-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-V-O-ADV] S V* O ADV -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-O-ADV" in 0.BR || "S-V-O-PP" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-O-ADV" in 0.AR || "S-V-O-PP" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-O-ADV-O2] S V* O ADV O2 -> CL
	? { "S-V-O-ADV-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

//Randall 11/14/08 for 1co1:8:1-1:8:15  46001008001 (old)
[S-V-O-ADV-O2-ADV] S V* O ADV O2 ADV -> CL
	? { "S-V-O-ADV-O2-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 9/2/09 1co1:8:1 (new)
[S-ADV-V-O-ADV-O2-ADV] S ADV V* O ADV O2 ADV -> CL
	? { "S-ADV-V-O-ADV-O2-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[S-V-O-ADV-ADV] S V* O ADV ADV -> CL
	? {  ( "S-V-O-ADV-PP" in 0.AR || "S-V-O-ADV-ADV" in 0.AR )
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-V-O-ADV-ADV-ADV] S V* O ADV ADV ADV -> CL
	? { "S-V-O-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-V-IO] S V* IO -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-IO" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "S-V-IO" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[S-V-IO-ADV] S V* IO ADV -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-IO-ADV" in 0.BR || "S-V-IO-PP" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-IO-ADV" in 0.AR || "S-V-IO-PP" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-IO-O] S V* IO O -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-IO-O" in 0.BR ) 
	     && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "S-V-IO-O" in 0.AR  && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-V-IO-O-ADV] S V* IO O ADV -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-IO-O-PP" in 0.BR || "S-V-IO-O-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-IO-O-PP" in 0.AR || "S-V-IO-O-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-V-IO-O-ADV-ADV] S V* IO O ADV ADV -> CL
	? { ( 0.Number == 1.Number 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc=="" && 4.Punc==""
	    && ! ( 1.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-V-IO-O-PP-PP" in 0.BR || "S-V-IO-O-ADV-ADV" in 0.BR ) 
	    && ! ( (5.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-V-IO-O-PP-PP" in 0.AR || "S-V-IO-O-ADV-ADV" in 0.AR )
	     && ! ( (5.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

[S-O-V] S O V* -> CL
	? { ( 0.Number == 2.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-O-V" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
        && ! ( 1.Type in {"Relative"} )
	    )
	  || ( "S-O-V" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[S-O-IO-V] S O IO V* -> CL
	? { "S-O-IO-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 6/2/08 for Doris for 1Th5:15:2	   
[ADV-S-O-ADV-IO-V] ADV S O ADV IO V* -> CL
	? { "ADV-S-O-ADV-IO-V" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

[IO-S-O-V] IO S O V* -> CL
	? { "IO-S-O-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[IO-S-O-V-ADV] IO S O V* ADV -> CL
	? { "IO-S-O-V-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-IO-S-O-V] ADV IO S O V* -> CL
	? { "ADV-IO-S-O-V" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) 
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   	   
[S-O-V-O2] S O V* O2 -> CL
	? { "S-O-V-O2" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) 
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 1/8/09 Heb7:28
[S-O-V-O2-ADV] S O V* O2 ADV -> CL
	? { "S-O-V-O2-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) 
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-O-V-O2] S ADV O V* O2 -> CL
	? { "S-ADV-O-V-O2" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) 
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-ADV-O-V-O2] S ADV ADV O V* O2 -> CL
	? { "S-ADV-ADV-O-V-O2" in 0.AR && ! ( (5.End-0.Start) in 0.NB ) 
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-ADV-O2-V-O] S ADV ADV O2 V* O -> CL
	? { "S-ADV-ADV-O2-V-O" in 0.AR && ! ( (5.End-0.Start) in 0.NB ) 
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

[S-O-V-IO] S O V* IO -> CL
	? { "S-O-V-IO" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

[S-O-V-IO-ADV] S O V* IO ADV -> CL
	? { "S-O-V-IO-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 5-18-08 for heb9:13:1-9:14:27 ("without spot" not well treated)
[S-ADV-O-V-IO] S ADV O V* IO -> CL
	? { "S-ADV-O-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 7/30/09 jud1:20
[S-ADV-O-ADV-V-ADV] S ADV O ADV V* ADV -> CL
	? { "S-ADV-O-ADV-V-ADV" in 0.AR 
	  && (5.End-0.Start) in 0.RB
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 1-8-09 for heb9:13:1-9:14:27 58009014007 (used over rules above & below)
[S-ADV-O-ADV-V-IO] S ADV O ADV V* IO -> CL
	? { "S-ADV-O-ADV-V-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 5-18-08 for heb9:13:1-9:14:27 (decided againt O2 for "without spot")
[S-ADV-O-O2-V-IO] S ADV O O2 V* IO -> CL
	? { "S-ADV-O-O2-V-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 5/20/11 heb9:13:1-9:14:27 (both rules above are no longer used in this passage; though not sure if there are used elsewhere)
[S-ADV-O-V-O2-IO] S ADV O V* O2 IO -> CL
	? { "S-ADV-O-V-O2-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 4/9/15 Eph4:17:1-4:19:12	   
[S-ADV-O-V-ADV-ADV] S ADV O V* ADV ADV -> CL
	? { "S-ADV-O-V-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-O-V-IO-ADV] S ADV O V* IO ADV -> CL
	? { "S-ADV-O-V-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-O-V-IO] ADV S O V* IO -> CL
	? { "ADV-S-O-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-S-O-V-IO-ADV] ADV S O V* IO ADV -> CL
	? { "ADV-S-O-V-IO-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

//Randall 06/29/08 for Gal2:7:1
[ADV-ADV-S-O-V-IO] ADV ADV S O V* IO -> CL
	? { "ADV-ADV-S-O-V-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 5-30-08 for 1th5:16:1
[S-O-ADV-IO-V] S O ADV IO V* -> CL
	? { "S-O-ADV-IO-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-O-ADV-V-IO] S O ADV V* IO -> CL
	? { "S-O-ADV-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-O-ADV-V-ADV] S O ADV V* ADV -> CL
	? { "S-O-ADV-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   	   
[S-O-V-ADV] S O V* ADV -> CL
	? { ( 0.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-O-V-ADV" in 0.BR || "S-O-V-PP" in 0.BR) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-O-V-ADV" in 0.AR || "S-O-V-PP" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[S-O-V-ADV-ADV] S O V* ADV ADV -> CL
	? { ( 0.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-O-V-PP-PP" in 0.BR || "S-O-V-ADV-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-O-V-PP-PP" in 0.AR || "S-O-V-ADV-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[S-O-V-ADV-ADV-ADV] S O V* ADV ADV ADV -> CL
	? { "S-O-V-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   	  
[S-O-V-ADV-ADV-ADV-ADV] S O V* ADV ADV ADV ADV -> CL
	? { "S-O-V-ADV-ADV-ADV-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[S-O-V-ADV-ADV-ADV-ADV-ADV] S O V* ADV ADV ADV ADV ADV -> CL
	? { "S-O-V-ADV-ADV-ADV-ADV-ADV" in 0.AR
	  && ! ( (7.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
	   }

[S-O-ADV-V] S O ADV V* -> CL
	? { ( 0.Number == 3.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-O-ADV-V" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "S-O-ADV-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-S-O-ADV-V] ADV S O ADV V* -> CL
	? { "ADV-S-O-ADV-V" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-S-O-ADV-V-ADV] ADV S O ADV V* ADV -> CL
	? { "ADV-S-O-ADV-V-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-ADV-S-O-ADV-V] ADV ADV S O ADV V* -> CL
	? { "ADV-ADV-S-O-ADV-V" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[S-ADV-O-V] S ADV O V* -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 3.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-PP-O-V" in 0.BR || "S-ADV-O-V" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-PP-O-V" in 0.AR || "S-ADV-O-V" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[S-ADV-ADV-O-V] S ADV ADV O V* -> CL
	? { "S-ADV-ADV-O-V" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[S-ADV-O-V-ADV] S ADV O V* ADV -> CL
	? { "S-ADV-O-V-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 4/15/2015 2Co1:23:1-1:23:17
[S-O2-O-V-ADV] S O2 O V* ADV -> CL
	? { "S-O2-O-V-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[S-ADV-ADV-O-V-ADV] S ADV ADV O V* ADV -> CL
	? { "S-ADV-ADV-O-V-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-O-ADV-V] S ADV O ADV V* -> CL
	? { "S-ADV-O-ADV-V" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[ADV-S-ADV-O-V] ADV S ADV O V* -> CL
	? { "ADV-S-ADV-O-V" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 5/30/09 2Pe2:4:1-2:10:12
[ADV-S-ADV-ADV-O-ADV-V] ADV S ADV ADV O ADV V* -> CL
	? { "ADV-S-ADV-ADV-O-ADV-V" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

// Randall 12/26/08 mrk3:6:1-3:6:15 41003006002
[ADV-S-ADV-ADV-O-V-ADV] ADV S ADV ADV O V* ADV -> CL
	? { "ADV-S-ADV-ADV-O-V-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	
[ADV-S-ADV-O-V-ADV] ADV S ADV O V* ADV -> CL
	? { "ADV-S-ADV-O-V-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[ADV-S-ADV-O-V-IO] ADV S ADV O V* IO -> CL
	? { "ADV-S-ADV-O-V-IO" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[S-ADV-V-IO-ADV] S ADV V* IO ADV -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "S-PP-V-IO-ADV" in 0.BR || "S-ADV-V-IO-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-PP-V-IO-ADV" in 0.AR || "S-ADV-V-IO-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[S-ADV-V-IO-O] S ADV V* IO O -> CL
	? { "S-ADV-V-IO-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 10/5/09 act8:18:1-8:19:15
[S-ADV-V-IO-O-ADV] S ADV V* IO O ADV -> CL
	? { "S-ADV-V-IO-O-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-S-ADV-V-IO-O] ADV S ADV V* IO O -> CL
	? { "ADV-S-ADV-V-IO-O" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[S-ADV-ADV-V-IO-O] S ADV ADV V* IO O -> CL
	? { "S-ADV-ADV-V-IO-O" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[ADV-S-P] ADV S P* -> CL
	? { "ADV-S-P" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "Verbless"; }
	
[ADV-ADV-S-P] ADV ADV S P* -> CL
	? { "ADV-ADV-S-P" in 0.AR 
	  && (3.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "Verbless"; }

/* // # I changed this sentence to my view and so this rule may not be needed
//Doris 6/17/08 for 2Th2:7:8-2:7:15
[ADV-S-ADV-ADV] ADV S* ADV ADV -> CL
	? { "ADV-S-ADV-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { 
		 .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
		 .ClType = "Verbless"; 
	   }
*/
	
[ADV-S-P-ADV] ADV S P* ADV -> CL
	? { "ADV-S-P-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc; .ClType = "Verbless";
	   }

[ADV-S-P-ADV-ADV] ADV S P* ADV ADV -> CL
	? { "ADV-S-P-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc; .ClType = "Verbless";
	   }
	   	   	   
[ADV-V-S-ADV-O] ADV V* S ADV O -> CL
	? { ( ! ( "ADV-V-S-ADV-O" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-V-S-ADV-O" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-V-S-ADV-O] ADV ADV V* S ADV O -> CL
	? { "ADV-ADV-V-S-ADV-O" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-V-S-ADV-O-ADV] ADV V* S ADV O ADV -> CL
	? { "ADV-V-S-ADV-O-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

[ADV-ADV-V-ADV] ADV ADV V* ADV -> CL
	? {  ( "ADV-ADV-V-ADV" in 0.AR || "ADV-ADV-V-PP" in 0.AR )
	  && ! ( (3.End-0.Start) in 0.NB )
      && ! ( "ADV-ADV-V-ADV-3" in 3.BR )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-ADV-V-ADV-S] ADV ADV V* ADV S -> CL
	? { "ADV-ADV-V-ADV-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
[ADV-ADV-V-ADV-ADV] ADV ADV V* ADV ADV -> CL
	? { "ADV-ADV-V-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-V-ADV-ADV-ADV] ADV ADV V* ADV ADV ADV -> CL
	? { "ADV-ADV-V-ADV-ADV-ADV" in 0.AR 
	  && (5.End-0.Start) in 0.RB
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

[ADV-ADV-ADV-V-ADV-ADV] ADV ADV ADV V* ADV ADV -> CL
	? { "ADV-ADV-ADV-V-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   	   
[ADV-ADV-ADV-V-ADV-ADV-ADV] ADV ADV ADV V* ADV ADV ADV -> CL
	? { "ADV-ADV-ADV-V-ADV-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	   
[ADV-ADV-V-ADV-IO] ADV ADV V* ADV IO -> CL
	? { "ADV-ADV-V-ADV-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-ADV-ADV-V-ADV] ADV ADV ADV V* ADV -> CL
	? {  "ADV-ADV-ADV-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[ADV-IO-ADV-V] ADV IO ADV V* -> CL
	? { "ADV-IO-ADV-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }	   	   	
[O-V] O V* -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == ""
	    && ! ( "O-V" in 0.BR ) 
	    && ( ! ( "O-V" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    && ! ( "V2Adjp" in 1.AR )
		&& ! ( "P-VC" in 0.AR )
        && ! ( "O-V-1" in 1.BR )
	    )
	  || ( ( (1.End-0.Start) in 0.RB && "O-V" in 0.AR )
	     && ! ( (1.End-0.Start) in 0.NB )
         && ! ( "O-V-1" in 1.BR )
	     )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 1.Punc;
	   }
		   
[O-IO-V] O IO V* -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == ""
	    && ! ( "O-IO-V" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "O-IO-V" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[O-ADV-IO-V] O ADV IO V* -> CL
	? { "O-ADV-IO-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-IO-V-ADV] O IO V* ADV -> CL
	? { (3.End-0.Start) in 0.RB && "O-IO-V-ADV" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-IO-V-ADV-ADV] O IO V* ADV ADV -> CL
	? { (4.End-0.Start) in 0.RB && "O-IO-V-ADV-ADV" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-O-IO-V] ADV O IO V* -> CL
	? { ! ( (3.End-0.Start) in 0.NB ) && "ADV-O-IO-V" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-IO-V-S] O IO V* S -> CL
	? {  "O-IO-V-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 4/1/09 act3:22:1
[O-IO-V-S-ADV] O IO V* S ADV -> CL
	? {  "O-IO-V-S-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-V-O2] O V* O2 -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == ""
	    && ! ( "O-V-O2" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "O-V-O2" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }

[O-V-O2-IO] O V* O2 IO -> CL
    ? { ! ( (3.End-0.Start) in 0.NB )
      && "O-V-O2-IO" in 0.AR
      } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-V-ADV-O2] O V* ADV O2 -> CL
	? { (3.End-0.Start) in 0.RB && "O-V-ADV-O2" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-ADV-V-O2] O ADV V* O2 -> CL
	? { "O-ADV-V-O2" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 4/28/09 Rom8:29:1
[O-ADV-V-O2-ADV] O ADV V* O2 ADV -> CL
	? { "O-ADV-V-O2-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-V-O2-ADV] O V* O2 ADV -> CL
	? { ! ( (3.End-0.Start) in 0.NB )
	  && "O-V-O2-ADV" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O2-V-O] O2 V* O -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == ""
	    && ! ( "O2-V-O" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "O2-V-O" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }

// Randall 1/19/09 Heb11:24:1-11:26:10
[O2-V-ADV-O] O2 V* ADV O -> CL
	? {  (3.End-0.Start) in 0.RB 
	  && "O2-V-ADV-O" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 4/16/09 2pe1:13:1-1:14:18
[O2-V-ADV-O-ADV] O2 V* ADV O ADV -> CL
	? {  (4.End-0.Start) in 0.RB 
	  && "O2-V-ADV-O-ADV" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O2-V-O-ADV] O2 V* O ADV -> CL
	? {  (3.End-0.Start) in 0.RB 
	  && "O2-V-O-ADV" in 0.AR
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-V-ADV] O V* ADV -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == ""
	    && ! ( "O-V-ADV" in 0.BR || "O-V-PP" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "O-V-ADV" in 0.AR || "O-V-PP" in 0.AR ) )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[O-V-ADV-ADV] O V* ADV ADV -> CL
	? { "O-V-ADV-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-V-ADV-ADV-ADV] O V* ADV ADV ADV -> CL
	? { "O-V-ADV-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[O-V-ADV-ADV-ADV-ADV] O V* ADV ADV ADV ADV -> CL
	? { "O-V-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   	   
[O-V-ADV-S] O V* ADV S -> CL
	? {  (3.End-0.Start) in 0.RB && "O-V-ADV-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 5-18-08 for 2jn1:1:1-1:2:14  63001001009
[O-V-ADV-S-ADV] O V* ADV S ADV -> CL
	? {  (4.End-0.Start) in 0.RB && "O-V-ADV-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 5-31-08 for 1Th2:14:1-2:16:14
[O-V-ADV-S-ADV-ADV] O V* ADV S ADV ADV -> CL
	? {  (5.End-0.Start) in 0.RB && "O-V-ADV-S-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 4/8/09 act7:44:1 (not used any more)
[O-V-ADV-S-ADV-ADV-ADV] O V* ADV S ADV ADV ADV -> CL
	? {  (6.End-0.Start) in 0.RB && "O-V-ADV-S-ADV-ADV-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

// Randall 8/17/11 act7:44:1
[O-ADV-V-ADV-S-ADV-ADV-ADV] O ADV V* ADV S ADV ADV ADV -> CL
	? {  (7.End-0.Start) in 0.RB && "O-ADV-V-ADV-S-ADV-ADV-ADV" in 0.AR
	  && ! ( (7.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
	   }
	   
[O-V-S] O V* S -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == ""
	    && 1.Number == 2.Number
	    && ! ( "O-V-S" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "O-V-S" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[O-V-S-IO] O V* S IO -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative") 
	    && */ 0.Punc == "" && 1.Punc == "" && 2.Punc == ""
	    && 1.Number == 2.Number
	    && ! ( "O-V-S-IO" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && "O-V-S-IO" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	      
[ADV-O-V-S] ADV O V* S -> CL
	? { ( 0.Punc == "" && 1.Punc == "" && 2.Punc == ""
	    && 2.Number == 3.Number
	    && ! ( "ADV-O-V-S" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && "ADV-O-V-S" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-O-V-S-ADV] ADV O V* S ADV -> CL
	? { "ADV-O-V-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 6/4/08 for Mat27:1:1 
[ADV-O-V-S-ADV-ADV] ADV O V* S ADV ADV -> CL
	? { "ADV-O-V-S-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-O-V-O2] ADV O V* O2 -> CL
	? { "ADV-O-V-O2" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB ) 
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 7/13/09 jhn8:25:7
[ADV-O-ADV-V-IO] ADV O ADV V* IO -> CL
	? { "ADV-O-ADV-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB ) 
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 3/18/09 jhn16:23:1 43016023002
[ADV-O-ADV-V-O2] ADV O ADV V* O2 -> CL
	? { "ADV-O-ADV-V-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB ) 
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 5/9/11 1pe3:21
[ADV-O-ADV-V-S-ADV] ADV O ADV V* S ADV -> CL
	? { "ADV-O-ADV-V-S-ADV" in 0.AR 
	  && (5.End-0.Start) in 0.RB 
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

//Randall 10/30/08 for Phm1:15:1
[ADV-O-V-ADV-O2] ADV O V* ADV O2 -> CL
	? { "ADV-O-V-ADV-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB ) 
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-O-V-IO] ADV O V* IO -> CL
	? { "ADV-O-V-IO" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

[ADV-O-V-IO-ADV] ADV O V* IO ADV -> CL
	? { "ADV-O-V-IO-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-V-S-ADV] O V* S ADV -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == "" && 2.Punc == ""
	    && 1.Number == 2.Number
	    && ! ( "O-V-S-ADV" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && "O-V-S-ADV" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 6-11-08 for Mat7:9:1
[O-V-S-O2] O V* S O2 -> CL
	? { "O-V-S-O2" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

//Randall 9/2/08 for rom3:25:1
[O-V-S-O2-ADV] O V* S O2 ADV -> CL
	? { "O-V-S-O2-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	  
[O-V-S-ADV-IO] O V* S ADV IO -> CL
	? { "O-V-S-ADV-IO" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-V-S-ADV-ADV] O V* S ADV ADV -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == "" && 2.Punc == "" && 3.Punc == ""
	    && 1.Number == 2.Number
	    && ! ( "O-V-S-ADV-PP" in 0.BR || "O-V-S-ADV-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( (4.End-0.Start) in 0.RB && ( "O-V-S-ADV-PP" in 0.AR || "O-V-S-ADV-ADV" in 0.AR ) )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 4/8/09 act7:15:6
[O-V-S-ADV-ADV-ADV] O V* S ADV ADV ADV -> CL
	? { "O-V-S-ADV-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[O-ADV-V] O ADV V* -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == ""
	    && ! ( "O-ADV-V" in 0.BR || "O-PP-V" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "O-ADV-V" in 0.AR || "O-PP-V" in 0.AR ))
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[O-ADV-V-S] O ADV V* S -> CL
	? { "O-ADV-V-S" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-ADV-V-ADV-S] O ADV V* ADV S -> CL
	? { "O-ADV-V-ADV-S" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-ADV-V-S-ADV] O ADV V* S ADV -> CL
	? { "O-ADV-V-S-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[O-ADV-V-S-ADV-ADV] O ADV V* S ADV ADV -> CL
	? { "O-ADV-V-S-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

[O-ADV-V-S-ADV-ADV-ADV] O ADV V* S ADV ADV ADV -> CL
	? { "O-ADV-V-S-ADV-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

[O-ADV-V-S-ADV-ADV-ADV-ADV] O ADV V* S ADV ADV ADV ADV -> CL
	? { "O-ADV-V-S-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (7.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc;
	   }

[O-ADV-V-S-ADV-ADV-ADV-ADV-ADV] O ADV V* S ADV ADV ADV ADV ADV -> CL
	? { "O-ADV-V-S-ADV-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (8.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 8.Punc;
	   }
	   
[O-ADV-V-IO] O ADV V* IO -> CL
	? { "O-ADV-V-IO" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-ADV-V-IO-ADV] O ADV V* IO ADV -> CL
	? { "O-ADV-V-IO-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-ADV-S-V-IO] O ADV S V* IO -> CL
	? { "O-ADV-S-V-IO" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 2/10/09 luk12:37:1-12:37:10 42012037005
[O-ADV-S-V-O2] O ADV S V* O2 -> CL
	? { "O-ADV-S-V-O2" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-ADV-ADV-S-V-IO] O ADV ADV S V* IO -> CL
	? { "O-ADV-ADV-S-V-IO" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-O-ADV-V] ADV O ADV V* -> CL
	? { "ADV-O-ADV-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-ADV-O-ADV-V] ADV ADV O ADV V* -> CL
	? { "ADV-ADV-O-ADV-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 3/7/14 for N1904 Heb11:11
[ADV-ADV-O-ADV-V-ADV] ADV ADV O ADV V* ADV -> CL
	? { "ADV-ADV-O-ADV-V-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[ADV-ADV-ADV-O-ADV-V] ADV ADV ADV O ADV V* -> CL
	? { "ADV-ADV-ADV-O-ADV-V" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	   
[O-ADV-ADV-V] O ADV ADV V* -> CL
	? { "O-ADV-ADV-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-ADV-ADV-ADV-V] O ADV ADV ADV V* -> CL
	? { "O-ADV-ADV-ADV-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-O-ADV-ADV-V] ADV O ADV ADV V* -> CL
	? { "ADV-O-ADV-ADV-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-O-ADV-V-ADV] ADV O ADV V* ADV -> CL
	? { "ADV-O-ADV-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-ADV-V-ADV] O ADV V* ADV -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == "" && 2.Punc == ""
	    && ! ( "O-ADV-V-ADV" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && "O-ADV-V-ADV" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 6/19/09 mat21:14:1
[O2-ADV-V-ADV] O2 ADV V* ADV -> CL
	? { "O2-ADV-V-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 9/8/11 act24:13
[O2-ADV-V-O] O2 ADV V* O -> CL
	? { "O2-ADV-V-O" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-ADV-ADV-V-ADV] O ADV ADV V* ADV -> CL
	? { "O-ADV-ADV-V-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 8/16/11 jhn8:25
[O-ADV-ADV-V-IO] O ADV ADV V* IO -> CL
	? { "O-ADV-ADV-V-IO" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-ADV-V-ADV-ADV] O ADV V* ADV ADV -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc == "" && 1.Punc == "" && 2.Punc == "" && 3.Punc == ""
	    && ! ( "O-ADV-V-ADV-ADV" in 0.BR ) 
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( (4.End-0.Start) in 0.RB && "O-ADV-V-ADV-ADV" in 0.AR )
	  } 
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[ADV-O-V] ADV O V* -> CL
	? { ( /* !1.Relative
	    && */ 0.Punc=="" && 1.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-O-V" in 0.BR || "PP-O-V" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "ADV-O-V" in 0.AR || "PP-O-V" in 0.AR ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[ADV-O-V-ADV] ADV O V* ADV -> CL
	? { ( /* !1.Relative
	    && */ 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "ADV-O-V-PP" in 0.BR || "ADV-O-V-ADV" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && ( "ADV-O-V-PP" in 0.AR || "ADV-O-V-ADV" in 0.AR ) )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-O-V-ADV-ADV] ADV O V* ADV ADV -> CL
	? { "ADV-O-V-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[ADV-O-V-ADV-S] ADV O V* ADV S -> CL
	? { "ADV-O-V-ADV-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-IO-V] ADV IO V* -> CL
	? { ( /* !1.Relative
	    && */ 0.Punc=="" && 1.Punc==""
	    && ! ( 2.Mood in {"Participle"} )
	    && ! ( "ADV-IO-V" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "ADV-IO-V" in 0.AR )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[ADV-IO-V-ADV] ADV IO V* ADV -> CL
	? { "ADV-IO-V-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB)
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

[ADV-IO-V-ADV-ADV] ADV IO V* ADV ADV -> CL
	? { "ADV-IO-V-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB)
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-IO-S-V] ADV IO S* V -> CL
	? { "ADV-IO-S-V" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-IO-V-O] ADV IO V* O -> CL
	? { "ADV-IO-V-O" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-IO-V-O-ADV] ADV IO V* O ADV -> CL
	? { "ADV-IO-V-O-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[ADV-IO-V-S-ADV] ADV IO V* S ADV -> CL
	? { "ADV-IO-V-S-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   	   
[ADV-IO-V-S-ADV-ADV] ADV IO V* S ADV ADV -> CL
	? { ( /* !1.Relative
	    && */ 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc=="" && 4.Punc==""
	    && ! ( 2.Mood in {"Participle"} )
	    && ! ( "ADV-IO-V-S-PP-ADV" in 0.BR || "ADV-IO-V-S-ADV-ADV" in 0.BR) 
	    && ! ( (5.End-0.Start) in 0.NB )
	    )
	  || ( (5.End-0.Start) in 0.RB && ( "ADV-IO-V-S-PP-ADV" in 0.AR || "ADV-IO-V-S-ADV-ADV" in 0.AR )  )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[O-V-IO] O V* IO -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 0.Punc=="" && 1.Punc==""
	    && ! ( "O-V-IO" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "O-V-IO" in 0.AR )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }

[O-V-IO-O2] O V* IO O2 -> CL
	? { "O-V-IO-O2" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	     
[O-V-IO-S] O V* IO S -> CL
	? { "O-V-IO-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-O-V-IO-S] ADV O V* IO S -> CL
	? { "ADV-O-V-IO-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-V-IO-S-ADV] O V* IO S ADV -> CL
	? { "O-V-IO-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 5-31-08 for 2co10:13 47010013014 (not used there any more)
[O-V-IO-S-ADV-ADV] O V* IO S ADV ADV -> CL
	? { "O-V-IO-S-ADV-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 6/2/11 for 2co10:13 47010013014
[O-V-IO-S-O2-ADV] O V* IO S O2 ADV -> CL
	? { "O-V-IO-S-O2-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-V-IO-ADV] O V* IO ADV -> CL
	? { "O-V-IO-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 9/09/09 2Co8:3:1-8:6:17
[O-V-IO-ADV-ADV] O V* IO ADV ADV -> CL
	? { "O-V-IO-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
		  
[O-S-V] O S V* -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "O-S-V" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "O-S-V" in 0.AR )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[O-ADV-S-V] O ADV S V* -> CL
	? { "O-ADV-S-V" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

[IO-O-S-V] IO O S V* -> CL
	? { "IO-O-S-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

//Randall 12/17/08 for Heb7:4:1
[IO-O-S-V-ADV] IO O S V* ADV -> CL
	? { "IO-O-S-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	  
[ADV-O-S-V] ADV O S V* -> CL
	? { "ADV-O-S-V" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[ADV-ADV-O-S-V] ADV ADV O S V* -> CL
	? { "ADV-ADV-O-S-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[ADV-O-S-V-ADV] ADV O S V* ADV -> CL
	? { "ADV-O-S-V-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[ADV-O-IO-S-V] ADV O IO S V* -> CL
	? { "ADV-O-IO-S-V" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-O-IO-S-V-ADV] ADV O IO S V* ADV -> CL
	? { "ADV-O-IO-S-V-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[O-S-V-IO] O S V* IO -> CL
	? { "O-S-V-IO" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-S-V-IO-ADV] O S V* IO ADV -> CL
	? { "O-S-V-IO-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	      
[O-S-V-O2] O S V* O2 -> CL
	? { "O-S-V-O2" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[ADV-O-S-V-O2] ADV O S V* O2 -> CL
	? { "ADV-O-S-V-O2" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[O-S-V-O2-ADV] O S V* O2 ADV -> CL
	? { "O-S-V-O2-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 6-11-08 for Jhn10:35:1
[ADV-O-S-V-O2-ADV] ADV O S V* O2 ADV -> CL
	? { "ADV-O-S-V-O2-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[O-S-V-ADV] O S V* ADV -> CL
	? { ( /* !( 0.Relative || 0.Type=="Relative")
	    && */ 1.Number == 2.Number
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( 2.Mood in {"Participle","Infinitive"} )
	    && ! ( "O-S-V-ADV" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && "O-S-V-ADV" in 0.AR )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[O-S-V-ADV-ADV] O S V* ADV ADV -> CL
	? { "O-S-V-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[O-S-V-ADV-ADV-ADV] O S V* ADV ADV ADV -> CL
	? { "O-S-V-ADV-ADV-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

[O-S-V-ADV-ADV-ADV-ADV] O S V* ADV ADV ADV ADV -> CL
	? { "O-S-V-ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (6.End-0.Start) in 0.NB )
	  }	  
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }

[IO-V] IO V* -> CL
	? { "IO-V" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 1.Punc;
	   }
	
[IO-S-V] IO S V* -> CL
	? { "IO-S-V" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }

//Randall 11/8/08 for rom4:4:1-4:4:12  45004004001
[IO-S-V-ADV] IO S V* ADV -> CL
	? { "IO-S-V-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-S-V-O] IO S V* O -> CL
	? { "IO-S-V-O" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

[IO-S-V-O-ADV] IO S V* O ADV -> CL
	? { "IO-S-V-O-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[IO-ADV-S-V-O] IO ADV S V* O -> CL
	? { "IO-ADV-S-V-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[ADV-IO-S-V-O] ADV IO S V* O -> CL
	? { "ADV-IO-S-V-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

// Randall 2/20/14 jhn8:5:1-8:5:10 1904Nestle
[ADV-S-IO-V-O] ADV S IO V* O -> CL
	? { "ADV-S-IO-V-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[IO-S-ADV-V] IO S ADV V* -> CL
	? { "IO-S-ADV-V" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-S-ADV-V-ADV] IO S ADV V* ADV -> CL
	? { "IO-S-ADV-V-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[IO-ADV-V] IO ADV V* -> CL
	? { "IO-ADV-V" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	
[IO-ADV-ADV-V] IO ADV ADV V* -> CL
	? { "IO-ADV-ADV-V" in 0.AR
	&& ! ( (3.End-0.Start) in 0.NB )
	}
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-ADV-ADV-V-ADV] IO ADV ADV V* ADV -> CL
	? { "IO-ADV-ADV-V-ADV" in 0.AR
	&& ! ( (4.End-0.Start) in 0.NB )
	}
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[IO-ADV-ADV-V-ADV-ADV] IO ADV ADV V* ADV ADV -> CL
	? { "IO-ADV-ADV-V-ADV-ADV" in 0.AR
	&& ! ( (5.End-0.Start) in 0.NB )
	}
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[IO-ADV-V-S] IO ADV V* S -> CL
	? { "IO-ADV-V-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-ADV-V-ADV] IO ADV V* ADV -> CL
	? { "IO-ADV-V-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-ADV-V-O] IO ADV V* O -> CL
	? { "IO-ADV-V-O" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[IO-ADV-ADV-V-O-ADV] IO ADV ADV V* O ADV -> CL
	? { "IO-ADV-ADV-V-O-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }

// Randall 4/3/09 act3:26:1
[IO-ADV-S-ADV-V-O-ADV] IO ADV S ADV V* O ADV -> CL
	? { "IO-ADV-S-ADV-V-O-ADV" in 0.AR
	  && ! ( (6.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 6.Punc;
	   }
	
[IO-V-O] IO V* O -> CL
	? { "IO-V-O" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[IO-V-O-ADV] IO V* O ADV -> CL
	? { "IO-V-O-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 7/23/09 1pe3:7:1
[IO-V-O-ADV-ADV] IO V* O ADV ADV -> CL
	? { "IO-V-O-ADV-ADV" in 0.AR
	  && (4.End-0.Start) in 0.RB
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-O-V] IO O V* -> CL
	? { "IO-O-V" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	
[IO-O-V-S] IO O V* S -> CL
	? { "IO-O-V-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[IO-O-V-ADV-S] IO O V* ADV S -> CL
	? { "IO-O-V-ADV-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	   
[IO-O-ADV-V-S] IO O ADV V* S -> CL
	? { "IO-O-ADV-V-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[IO-O-ADV-V] IO O ADV V* -> CL
	? { "IO-O-ADV-V" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[ADV-IO-O-V] ADV IO O V* -> CL
	? { "ADV-IO-O-V" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }

// Randall 9/23/10 Heb11:11:1
[ADV-IO-O-ADV-V-ADV] ADV IO O ADV V* ADV -> CL
	? { "ADV-IO-O-ADV-V-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[IO-O-V-ADV] IO O V* ADV -> CL
	? { "IO-O-V-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[S-IO-O-V] S IO O V* -> CL
	? { "S-IO-O-V" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[S-IO-O-ADV-V] S IO O ADV V* -> CL
	? { "S-IO-O-ADV-V" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }
	
[IO-V-S] IO V* S -> CL
	? { "IO-V-S" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	
[ADV-IO-V-S] ADV IO V* S -> CL
	? { "ADV-IO-V-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-V-S-O] IO V* S O-> CL
	? { "IO-V-S-O" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	
[IO-V-S-ADV] IO V* S ADV -> CL
	? { ( "IO-V-S-PP" in 0.AR || "IO-V-S-ADV" in 0.AR )
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[IO-V-S-ADV-ADV] IO V* S ADV ADV -> CL
	? { "IO-V-S-ADV-ADV" in 0.AR
	  && (4.End-0.Start) in 0.RB
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[IO-V-S-ADV-ADV-ADV] IO V* S ADV ADV ADV -> CL
	? { "IO-V-S-ADV-ADV-ADV" in 0.AR
	  && (5.End-0.Start) in 0.RB
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc;
	   }
	
[IO-V-S-ADV-O] IO V* S ADV O -> CL
	? { "IO-V-S-ADV-O" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc;
	   }

[IO-V-ADV] IO V* ADV -> CL
	? { "IO-V-ADV" in 0.AR || "IO-V-PP" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc;
	   }
	   
[IO-V-ADV-O] IO V* ADV O -> CL
	? { "IO-V-ADV-O" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }
	   
[IO-V-ADV-ADV] IO V* ADV ADV -> CL
	? { "IO-V-ADV-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
	     { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc;
	   }


// Nominal clauses with "be", "become"

[S-VC] S* VC -> CL
	? { "S-VC" in 0.AR && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 1.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-VC-P] S VC P* -> CL
	? { ( ! ( "S-VC-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "S-VC-P" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-VC-P-ADV] S VC P* ADV -> CL
	? { ( ! ( "S-VC-P-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
		&& ! ( "P-VC-ADV-ADV" in 0.AR )
	    )
	  || ( "S-VC-P-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-VC-P-ADV-ADV] S VC P* ADV ADV -> CL
	? { "S-VC-P-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

//Randall 11/11/08 for Act22:2:12
[S-VC-P-ADV-ADV-ADV] S VC P* ADV ADV ADV -> CL
	? { "S-VC-P-ADV-ADV-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[ADV-ADV-VC-S-P-ADV] ADV ADV VC S P* ADV -> CL
      ? { "ADV-ADV-VC-S-P-ADV" in 0.AR 
        && (5.End-0.Start) in 0.RB
        }
      >> { .RB = 0.RB; .NB = 0.NB;
           .Mood = 2.Mood;
		   		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		   { .Type="Relative"; }
           .Punc = 5.Punc; 
         }

[ADV-S-P-ADV-VC] ADV S P* ADV VC -> CL
	? { "ADV-S-P-ADV-VC" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
         .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
	     .Punc = 4.Punc;
	   }

/*
[ADV-S-VC] ADV S* VC -> CL
	? { "ADV-S-VC" in 0.AR && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
	     .Punc = 2.Punc;
	   }
 */ 

/*
[ADV-S-VC-ADV] ADV S* VC ADV -> CL
	? { "ADV-S-VC-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc;
	   }
*/

[ADV-S-VC-P] ADV S VC P* -> CL
	? { ( ! ( "ADV-S-VC-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-S-VC-P" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[ADV-S-VC-P-ADV] ADV S VC P* ADV -> CL
	? { "ADV-S-VC-P-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-ADV-VC-P] S ADV VC P* -> CL
	? { ( ! ( "S-ADV-VC-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "S-ADV-VC-P" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc;
	   }
	   
[ADV-S-ADV-VC-P] ADV S ADV VC P* -> CL
	? { "ADV-S-ADV-VC-P" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc;
	   }

//Randall 12/17/08 for Heb7:1:1
[S-ADV-ADV-VC-P-ADV] S ADV ADV VC P* ADV -> CL
	? { "S-ADV-ADV-VC-P-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

// Randall 6/13/09 mat10:37:1
[S-ADV-VC-ADV-P] S ADV VC ADV P* -> CL
	? { "S-ADV-VC-ADV-P" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-ADV-VC-P-ADV] S ADV VC P* ADV -> CL
	? { "S-ADV-VC-P-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[S-ADV-P-VC] S ADV P* VC -> CL
	? { "S-ADV-P-VC" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc; 
	   }

// Randall 5-28-08 for heb2:14:1-2:15:13  58002015004
[S-ADV-ADV-P-VC] S ADV ADV P* VC -> CL
	? { "S-ADV-ADV-P-VC" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }

// Randall 5-28-08 added in response to request for heb2:14:1 (but not applied as not applicable)
[S-ADV-ADV-P-VC-ADV] S ADV ADV P* VC ADV -> CL
	? { "S-ADV-ADV-P-VC-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 5.Punc;
	   }

// Randall 9-21-08 for col4:6:1
[S-ADV-P-ADV-ADV] S ADV P* ADV ADV -> CL
	? { "S-ADV-P-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }
	
[ADV-S-ADV-P-VC] ADV S ADV P* VC -> CL
	? { "ADV-S-ADV-P-VC" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }

// Randall 9/25/09 jms2:17
[ADV-S-ADV-P-VC-ADV] ADV S ADV P* VC ADV -> CL
	? { "ADV-S-ADV-P-VC-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 5.Punc;
	   }

[S-ADV-P-VC-ADV] S ADV P* VC ADV -> CL
	? { "S-ADV-P-VC-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }
	
[S-ADV-P-ADV-VC] S ADV P* ADV VC -> CL
	? { "S-ADV-P-ADV-VC" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }
	   
[ADV-ADV-S-ADV-VC-ADV-P] ADV ADV S ADV VC ADV P* -> CL
	? { "ADV-ADV-S-ADV-VC-ADV-P" in 0.AR && ! ( (6.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 6.Punc;
	   }
	   
[S-ADV-VC-P-ADV-ADV] S ADV VC P* ADV ADV -> CL
	? { "S-ADV-VC-P-ADV-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB ) 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

//Randall 11/11/08 for Act22:2:12 (only case)
[S-VC-ADV] S* VC ADV -> CL //P elided and carried over from previous clause
	? { "S-VC-ADV" in 0.AR 
	  && (2.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 2.Punc;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	
[S-VC-ADV-P] S VC ADV P* -> CL
	? { ( ! ( "S-VC-PP-P" in 0.BR || "S-VC-ADV-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "S-VC-PP-P" in 0.AR || "S-VC-ADV-P" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

//Randall 10/29/08 for Tit1:7:1
[S-VC-ADV-P-ADV] S VC ADV P* ADV -> CL
	? { "S-VC-ADV-P-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

// Randall 4/29/09 1co1:30:1
[S-VC-ADV-ADV-P] S VC ADV ADV P* -> CL
	? { "S-VC-ADV-ADV-P" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-VC-ADV-ADV-P-ADV] S VC ADV ADV P* ADV -> CL
	? { "S-VC-ADV-ADV-P-ADV" in 0.AR 
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	
[S-P-VC] S P* VC -> CL
	? { ( ! ( 1.Rule in {"pp2P"} )
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( "S-P-VC" in 0.BR ) 
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "S-P-VC" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-P-ADV-VC] S P* ADV VC -> CL
	? { (3.End-0.Start) in 0.RB && "S-P-ADV-VC" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-P-ADV-VC-ADV] S P* ADV VC ADV -> CL
	? { "S-P-ADV-VC-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

//Randall 12/9/08 for Heb3:5:1
[S-P-ADV-ADV-ADV] S P* ADV ADV ADV -> CL
	? { "S-P-ADV-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-P-ADV-ADV-VC] S P* ADV ADV VC -> CL
	? { "S-P-ADV-ADV-VC" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

	   
[S-P-VC-ADV] S P* VC ADV -> CL
	? { ( ! ( 1.Rule in {"pp2P"} )
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "S-P-VC-ADV" in 0.BR || "S-P-VC-PP" in 0.BR ) 
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && ( "S-P-VC-ADV" in 0.AR || "S-P-VC-PP" in 0.AR ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[S-P-VC-ADV-ADV] S P* VC ADV ADV -> CL
	? { "S-P-VC-ADV-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[P-S-VC] P* S VC -> CL
	? { ( ! ( "P-S-VC" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
		&& ! ( "ADV-S-V" in 0.AR )
	    )
	  || ( "P-S-VC" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   } 
	   
[P-S-VC-ADV] P* S VC ADV -> CL
	? { "P-S-VC-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   } 

[P-S-ADV-VC] P* S ADV VC -> CL
	? { ( ! ( "P-S-ADV-VC" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "P-S-ADV-VC" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
		 .Punc = 3.Punc; 
	   } 

// Randall 7/15/11 luk8:30
[P-ADV-S-VC] P* ADV S VC -> CL
	? { "P-ADV-S-VC" in 0.AR 
	  && (3.End-0.Start) in 0.RB 
	  }
	>> { .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
		 .Punc = 3.Punc; 
	   } 
	   
[P-ADV-S-ADV-VC] P* ADV S ADV VC -> CL
	? { "P-ADV-S-ADV-VC" in 0.AR 
	  && (4.End-0.Start) in 0.RB 
	  }
	>> { .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
		 .Punc = 4.Punc; 
	   } 
	   
[P-ADV-ADV-S-VC] P* ADV ADV S VC -> CL
	? { "P-ADV-ADV-S-VC" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   } 
	   
[ADV-VC-P-S] ADV VC P* S -> CL
	? { ( ! ( "PP-VC-P-S" in 0.BR || "ADV-VC-P-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "PP-VC-P-S" in 0.AR || "ADV-VC-P-S" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-ADV-VC-P-S] ADV ADV VC P* S -> CL
	? { "ADV-ADV-VC-P-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-VC-P-S-ADV] ADV VC P* S ADV -> CL
	? { "ADV-VC-P-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

// Randall 3/7/14 for N1904 Heb3:12
[ADV-ADV-VC-P-S-ADV] ADV ADV VC P* S ADV -> CL
	? { "ADV-ADV-VC-P-S-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-S-P-VC] ADV S P* VC -> CL
	? { ( ! ( "PP-S-P-VC" in 0.BR || "ADV-S-P-VC" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "PP-S-P-VC" in 0.AR || "ADV-S-P-VC" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-S-P-VC-ADV] ADV S P* VC ADV -> CL
	? { "ADV-S-P-VC-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[ADV-ADV-S-VC-P] ADV ADV S VC P* -> CL
	? { "ADV-ADV-S-VC-P" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
	     .Punc = 4.Punc;
	   }

[ADV-ADV-S-VC-P-ADV] ADV ADV S VC P* ADV -> CL
	? { "ADV-ADV-S-VC-P-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
	     .Punc = 5.Punc;
	   }

/*	      
[ADV-ADV-S-VC-ADV] ADV ADV S* VC ADV -> CL
	? { "ADV-ADV-S-VC-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc;
	   }
*/
	   
[ADV-ADV-P-VC] ADV ADV P* VC -> CL
	? { "ADV-ADV-P-VC" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc;
	   }

[ADV-ADV-P-VC-S] ADV ADV P* VC S -> CL
	? { "ADV-ADV-P-VC-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc;
	   }
	      
[ADV-ADV-P-VC-ADV] ADV ADV P* VC ADV -> CL
	? { "ADV-ADV-P-VC-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc;
	   }
	     
[ADV-ADV-S-P-VC] ADV ADV S P* VC -> CL
	? { "ADV-ADV-S-P-VC" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
	     .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

// Randall 9/25/09 jms2:26
[ADV-ADV-S-ADV-P-VC] ADV ADV S ADV P* VC -> CL
	? { "ADV-ADV-S-ADV-P-VC" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 5.Punc;
	     .Mood = 5.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[P-VC] P* VC -> CL
	? { ( ! ( "P-VC" in 0.BR ) 
	    && 0.Punc==""
	    && ! ( (1.End-0.Start) in 0.NB )
		&& ! ( "ADV-V" in 0.AR )
	    )
	  || ( "P-VC" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 1.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	
[P-VC-S] P* VC S -> CL
	? { ( ! ( "P-VC-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "P-VC-S" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[P-VC-ADV-S-ADV] P* VC ADV S ADV -> CL
	? { "P-VC-ADV-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Mood= 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }
	
[P-VC-S-ADV] P* VC S ADV -> CL
	? { "P-VC-S-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

//Randall 9/15/08 Col1:24:1
[P-VC-S-ADV-ADV] P* VC S ADV ADV -> CL
	? { "P-VC-S-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[P-VC-ADV] P* VC ADV -> CL
	? { ( ! ( "P-VC-PP" in 0.BR || "P-VC-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "P-VC-PP" in 0.AR || "P-VC-ADV" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[P-VC-ADV-S] P* VC ADV S -> CL
	? { "P-VC-ADV-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[P-ADV-ADV-VC] P* ADV ADV VC -> CL
	? { "P-ADV-ADV-VC" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }
	
[P-ADV-VC] P* ADV VC -> CL
	? { "P-ADV-VC" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 2.Punc; 
	   }
	
[P-ADV-VC-ADV] P* ADV VC ADV -> CL
	? { "P-ADV-VC-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 3.Punc; 
	   }
	
[P-ADV-VC-S] P* ADV VC S -> CL
	? { "P-ADV-VC-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 3.Punc; 
	   }
	
[P-ADV-VC-S-ADV] P* ADV VC S ADV -> CL
	? { "P-ADV-VC-S-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }
	   
[P-VC-ADV-ADV] P* VC ADV ADV -> CL
	? { "P-VC-ADV-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[P-VC-ADV-ADV-ADV] P* VC ADV ADV ADV -> CL
	? { "P-VC-ADV-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[ADV-P-VC] ADV P* VC -> CL
	? { ( ! ( "ADV-P-VC" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-P-VC" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }
	   
[ADV-P-ADV-VC] ADV P* ADV VC -> CL
	? { "ADV-P-ADV-VC" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }

[ADV-P-VC-ADV-S] ADV P* VC ADV S -> CL
	? { "ADV-P-VC-ADV-S" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

// Randall 9/8/11 act24:11
[ADV-P-VC-ADV-S-ADV] ADV P* VC ADV S ADV -> CL
	? { "ADV-P-VC-ADV-S-ADV" in 0.AR
	  && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 5.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-ADV-P-ADV-VC] ADV ADV P* ADV VC -> CL
	? { "ADV-ADV-P-ADV-VC" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }

// Randall 9/4/11 act20:18
[ADV-ADV-P-ADV-VC-ADV] ADV ADV P* ADV VC ADV -> CL
	? { "ADV-ADV-P-ADV-VC-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 5.Punc; 
	     .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }
	   
[ADV-P-ADV-ADV-VC] ADV P* ADV ADV VC -> CL
	? { "ADV-P-ADV-ADV-VC" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }

[ADV-P-ADV-VC-S] ADV P* ADV VC S -> CL
	? { "ADV-P-ADV-VC-S" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 4.Punc; 
	   }
	
[ADV-ADV-P-ADV-VC-S] ADV ADV P* ADV VC S -> CL
	? { "ADV-ADV-P-ADV-VC-S" in 0.AR && ! ( (5.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Mood = 4.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 5.Punc;
	   }
	      
[ADV-P-S-VC] ADV P* S VC -> CL
	? { "ADV-P-S-VC" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc; 
	   }
	   
[ADV-P-S-VC-ADV] ADV P* S VC ADV -> CL
	? { "ADV-P-S-VC-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 3.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc; 
	   }
	        
[ADV-P-VC-S] ADV P* VC S -> CL
	? { ( ! ( "ADV-P-VC-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-P-VC-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-P-VC-S-ADV] ADV P* VC S ADV -> CL
	? { "ADV-P-VC-S-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-P-VC-ADV] ADV P* VC ADV -> CL
	? { ( ! ( "ADV-P-VC-PP" in 0.BR || "ADV-P-VC-ADV" in 0.BR) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "ADV-P-VC-PP" in 0.AR || "ADV-P-VC-ADV" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-P-VC-ADV-ADV] ADV P* VC ADV ADV -> CL
	? { "ADV-P-VC-ADV-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-S-ADV-P] VC S ADV P* -> CL
	? { "VC-S-ADV-P" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc;
	   }

// Randall 3/31/09 act2:1:1 (only case)
[ADV-VC-S-ADV-P] ADV VC S ADV P* -> CL
	? { "ADV-VC-S-ADV-P" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc;
	   }
	   
[ADV-VC-S-P-ADV] ADV VC S P* ADV -> CL
	? { "ADV-VC-S-P-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc;
	   }
	   
[VC-S-P] VC S P* -> CL
	? { ( ! ( "VC-S-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "VC-S-P" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }
	   
[VC-P-S] VC P* S -> CL
	? { "VC-P-S" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;     
	   }
	   
[VC-P-ADV-S] VC P* ADV S -> CL
	? { "VC-P-ADV-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;     
	   }
	   
[VC-P-ADV-S-ADV] VC P* ADV S ADV -> CL
	? { "VC-P-ADV-S-ADV" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 4.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;     
	   }
	
[VC-P] VC P* -> CL
	? { ( ! ( "VC-P" in 0.BR ) 
	    && 0.Punc==""
	    && ! ( 1.Mood == "Participle" )
	    && ( ! ( "VC-P" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( (1.End-0.Start) in 0.RB && "VC-P" in 0.AR )
	  } 
	>> { 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	     .Punc = 1.Punc;
	   }

[VC-P-S-ADV] VC P* S ADV -> CL
	? { "VC-P-S-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	      
[VC-P-S-ADV-ADV] VC P* S ADV ADV -> CL
	? { ( ! ( "VC-P-S-PP-ADV" in 0.BR || "VC-P-S-ADV-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( ( "VC-P-S-PP-ADV" in 0.AR || "VC-P-S-ADV-ADV" in 0.AR )
	     && ! ( (4.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[VC-P-ADV] VC P* ADV -> CL
	? { ( ! ( "VC-P-ADV" in 0.BR || "VC-P-PP" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "VC-P-ADV" in 0.AR || "VC-P-PP" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .RB = 0.RB; .NB = 0.NB;
	   }
	      
[VC-P-ADV-ADV] VC P* ADV ADV -> CL
	? { ( ! ( "VC-P-PP-ADV" in 0.BR || "VC-P-ADV-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( ( "VC-P-PP-ADV" in 0.AR || "VC-P-ADV-ADV" in 0.AR )
	     && ! ( (3.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[VC-P-ADV-ADV-ADV] VC P* ADV ADV ADV -> CL
	? { "VC-P-ADV-ADV-ADV" in 0.AR
	     && ! ( (4.End-0.Start) in 0.NB )
	     
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	
[VC-ADV-P] VC ADV P* -> CL
	? { ( ! ( "VC-ADV-P" in 0.BR || "VC-PP-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( ( "VC-ADV-P" in 0.AR || "VC-PP-P" in 0.AR )
	     && ! ( (2.End-0.Start) in 0.NB )
	     )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-ADV-P-S] VC ADV P* S -> CL
	? { "VC-ADV-P-S" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-ADV-P-S-ADV] VC ADV P* S ADV -> CL
	? { "VC-ADV-P-S-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  } 
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-ADV-P-ADV] VC ADV P* ADV -> CL
	? { "VC-ADV-P-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-ADV-ADV-P] VC ADV ADV P* -> CL
	? { "VC-ADV-ADV-P" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

[VC-ADV-S-P] VC ADV S P* -> CL
	? { "VC-ADV-S-P" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-ADV-S-P-ADV] VC ADV S P* ADV -> CL
	? { "VC-ADV-S-P-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 4.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-ADV-ADV-S-P] VC ADV ADV S P* -> CL
	? { "VC-ADV-ADV-S-P" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	
[VC-S-P-ADV] VC S P* ADV -> CL
	? { ( ! ( "VC-S-P-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    ) 
	  || ( "VC-S-P-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[VC-S-P-ADV-ADV] VC S P* ADV ADV -> CL
	? { "VC-S-P-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc; 
	     .Mood = 0.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
	   
[ADV-VC-P-ADV] ADV VC P* ADV -> CL
	? { ( ! ( "ADV-VC-P-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-VC-P-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
	     .Punc = 3.Punc;
	   }

//Randall 06/30/08 for Gal5:26:1
[ADV-VC-P-ADV-ADV] ADV VC P* ADV ADV -> CL
	? { ( ! ( "ADV-VC-P-ADV-ADV" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc=="" && 3.Punc==""
	    && ! ( (4.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-VC-P-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
	     .Punc = 3.Punc;
	   }

// Randall 7/11/09 act25:16:1
[ADV-VC-P-ADV-S] ADV VC P* ADV S -> CL
	? { "ADV-VC-P-ADV-S" in 0.AR
		&& ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; } 
	     .Punc = 3.Punc;
	   }

[ADV-ADV-VC-P] ADV ADV VC P* -> CL
	? { "ADV-ADV-VC-P" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
		 .Punc = 3.Punc; 
	   }

[ADV-ADV-VC-ADV-P] ADV ADV VC ADV P* -> CL
	? { "ADV-ADV-VC-ADV-P" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc; 
	   }
	   
[ADV-ADV-VC-P-ADV] ADV ADV VC P* ADV -> CL
	? { "ADV-ADV-VC-P-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc; 
	   }
	    
[ADV-ADV-VC-P-ADV-S] ADV ADV VC P* ADV S -> CL
	? { "ADV-ADV-VC-P-ADV-S" in 0.AR && ! ( (5.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 5.Punc;
	   }

[ADV-VC-S-P] ADV VC S P* -> CL
	? { ( ! ( "ADV-VC-S-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-VC-S-P" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc; 
	   }
	   
[ADV-ADV-VC-S-P] ADV ADV VC S P* -> CL
	? { "ADV-ADV-VC-S-P" in 0.AR 
	  && (4.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc; 
	   }

[ADV-VC-ADV-P] ADV VC ADV P* -> CL
	? { ( 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( "ADV-VC-PP-P" in 0.BR || "ADV-VC-ADV-P" in 0.BR)
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( (3.End-0.Start) in 0.RB && ( "ADV-VC-PP-P" in 0.AR || "ADV-VC-ADV-P" in 0.AR ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 3.Punc; 
	   }

//Randall 10/13/08 for heb5:9:1-5:10:9  58005009002
[ADV-VC-ADV-P-ADV] ADV VC ADV P* ADV -> CL
	? { "ADV-VC-ADV-P-ADV" in 0.AR 
	  && (4.End-0.Start) in 0.RB
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 4.Punc; 
	   }
	   
[ADV-VC-P] ADV VC P* -> CL
	? { "ADV-VC-P" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	     .Punc = 2.Punc; 
	   }


//PREDICATE-ELIDED CLAUSES

/* // # This & the rest below are not valid rules unless predicate elided	   
[ADV-VC-S] ADV VC S* -> CL
	? { ( 0.Punc=="" && 1.Punc==""
	    && ! ( "ADV-VC-S" in 0.BR || "PP-VC-S" in 0.BR)
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "ADV-VC-S" in 0.AR || "PP-VC-S" in 0.AR ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 1.Mood;
	     .Punc = 2.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/

/* This and all rules with VC but no P need to be manually changed #
All cases need to be reevaluated as to whether the ADV should be P
In cases where the verb involved (EIMI and GINOMAI) is existential
rather than merely relational, change VC to V	   
[S-ADV-VC] S* ADV VC -> CL
	? { ( ! ( "S-ADV-VC" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
		&& ! ( "S-P-VC" in 0.AR )
	    )
	  || ( "S-ADV-VC" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc;
	     .Mood = 2.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/

// #	Legit with carried over P (1 case only)
[VC-S] VC S* -> CL
	? { ( ! ( "VC-S" in 0.BR ) 
	    && 0.Punc==""
		&& ( "np2S" in 1.AR || "Np2S" in 1.AR || "CL2S" in 1.AR )
	    && ( ! ( "VC-S" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( ( (1.End-0.Start) in 0.RB && "VC-S" in 0.AR )
	     && ! ( (1.End-0.Start) in 0.NB )
	     )
	  } 
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 0.Mood;
	     .Punc = 1.Punc;
	   }

/* // #	   
[VC-S-ADV] VC S* ADV -> CL
	? { "VC-S-ADV" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 0.Mood;
	     .Punc = 2.Punc;
	   }
*/

/* // #	   
[VC-ADV-ADV-S] VC ADV ADV S* -> CL
	? { "VC-ADV-ADV-S" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; 
	     .Mood = 0.Mood;
	   }
*/

/* // #	   
[VC-ADV-S] VC* ADV S -> CL
	? { ( "VC-ADV-S" in 0.AR || "VC-PP-S" in 0.AR )
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; 
	     .Mood = 0.Mood;
	   }
*/

// #	   Legit with carried over P
[ADV-VC] ADV VC* -> CL
	? { "ADV-VC" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 1.Punc;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }

/* // #	   
[ADV-VC-ADV] ADV VC* ADV -> CL
	? { "ADV-VC-ADV" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
		 .Punc = 2.Punc;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/

/* // #
[VC-ADV] VC* ADV -> CL
	? { "VC-ADV" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; 
		 .Mood = 0.Mood;
	   }
*/

/* // #	   
[ADV-VC-ADV-S] ADV VC* ADV S -> CL
	? { "ADV-VC-ADV-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/

/* // #	   
[ADV-VC-ADV-S-ADV] ADV VC* ADV S ADV -> CL
	? { ( "ADV-VC-ADV-S-PP" in 0.AR || "ADV-VC-ADV-S-ADV" in 0.AR )
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc;
		 .Mood = 1.Mood;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/

/* // #	   
[VC-S-ADV-ADV] VC S* ADV ADV -> CL
	? { "VC-S-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 0.Mood;
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/

/* // #	   
[ADV-VC-S-ADV] ADV VC S* ADV -> CL
	? { "ADV-VC-S-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 1.Mood;
	     .Punc = 3.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/
 
/* // #	   
[ADV-VC-S-ADV-ADV] ADV VC S* ADV ADV -> CL
	? { "ADV-VC-S-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Mood = 1.Mood;
	     .Punc = 4.Punc;
		 if ( 0.Lemma in {"o(/s","o(/stis"} || 0.UnicodeLemma in {"ὅς","ὅστις"} || ( 0.Relative || 0.Type=="Relative")  )
		 { .Type="Relative"; }
	   }
*/

	   
// VERBLESS CLAUSES

[P2CL] P* -> CL
	? { "P2CL" in 0.AR 
		&& ! ( 0.Rule in { "CL2P" } )
	  }
	>> { .ClType = "Verbless";
	   }

[S-P] S P* -> CL
	? { ( 0.Case == "Nominative"
	    && 1.Case == "Nominative"
	    && 0.Number == 1.Number
	    && 0.Punc==""
	    && ! ( "S-P" in 0.BR )
	    && ! ( "P-S" in 0.AR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( (1.End-0.Start) in 0.RB && "S-P" in 0.AR )
	  }
	>> { .Punc = 1.Punc; .ClType = "Verbless"; 
	     .RB = 0.RB; .NB = 0.NB;
	   }
	
[S-P-ADV] S P* ADV -> CL
	? { ( 0.Case == "Nominative"
	    && 1.Case == "Nominative"
	    && 0.Number == 1.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( "S-P-ADV" in 0.BR || "S-P-PP" in 0.BR )
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && ( "S-P-ADV" in 0.AR || "S-P-PP" in 0.AR ) )
	  }
	>> { .Punc = 2.Punc; 
	     .RB = 0.RB; .NB = 0.NB; .ClType = "Verbless";
	   }
	
[S-P-ADV-ADV] S P* ADV ADV -> CL
	? { "S-P-ADV-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; .ClType = "Verbless";
	     .RB = 0.RB; .NB = 0.NB;
	   }
	
[S-ADV-P] S ADV P* -> CL
	? { ( 0.Case == "Nominative"
	    && 2.Case == "Nominative"
	    && 0.Number == 2.Number
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( "S-ADV-P" in 0.BR )
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( (2.End-0.Start) in 0.RB && "S-ADV-P" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "Verbless"; 
		 .Punc = 2.Punc;
	   }
	
[S-ADV-ADV-P] S ADV ADV P* -> CL
	? { "S-ADV-ADV-P" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "Verbless"; 
		 .Punc = 3.Punc;
	   }
	
[ADV-S-ADV-P] ADV S ADV P* -> CL
	? { "ADV-S-ADV-P" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "Verbless"; 
		 .Punc = 3.Punc;
	   }

[P-S] P* S -> CL
	? { (1.End-0.Start) in 0.RB && "P-S" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; .ClType = "Verbless"; } 
	
[P-S-ADV] P* S ADV -> CL
	? { (2.End-0.Start) in 0.RB && ( "P-S-PP" in 0.AR || "P-S-ADV" in 0.AR )
	  }
	>> { .Punc = 2.Punc; 
	     .ClType = "Verbless";
	   }
	
[P-S-ADV-ADV] P* S ADV ADV -> CL
	? { "P-S-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .ClType = "Verbless";
	   }
		
[ADV-P] ADV P* -> CL
	? { ( "PP-P" in 0.AR || "ADV-P" in 0.AR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .ClType = "Verbless";
	     .Punc = 1.Punc;
	   }
	
[ADV-ADV-P] ADV ADV P* -> CL
	? { "ADV-ADV-P" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .ClType = "Verbless"; 
	     .Punc = 2.Punc;
	   }
	   
[ADV-ADV-P-ADV] ADV ADV P* ADV -> CL
	? { "ADV-ADV-P-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .ClType = "Verbless";
	     .Punc = 3.Punc; 
	   }
	
[ADV-P-ADV] ADV P* ADV -> CL
	? { "ADV-P-ADV" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc;
	     .ClType = "Verbless";
	   }

//Randall 10/27/08 for 2Tm3:2:1
[ADV-P-ADV-ADV] ADV P* ADV ADV -> CL
	? { "ADV-P-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; 
	     .RB = 0.RB; .NB = 0.NB;
		 .ClType = "Verbless";
	   }

[ADV-P-S] ADV P* S -> CL
	? { ( ! ( "ADV-P-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-P-S" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc; .ClType = "Verbless";
	   }
	   
[ADV-P-S-ADV] ADV P* S ADV -> CL
	? { "ADV-P-S-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; .ClType = "Verbless";
	   }
	
[P-ADV] P* ADV -> CL
	? { "P-ADV" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .Punc = 1.Punc; 
	     .ClType = "Verbless";
	   }
	
[P-ADV-ADV] P* ADV ADV -> CL
	? { "P-ADV-ADV" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Punc = 2.Punc; 
	     .ClType = "Verbless";
	   }

// Randall 4/16/09 1pe3:14:8-3:16:21
[P-ADV-ADV-ADV] P* ADV ADV ADV -> CL
	? { "P-ADV-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc; 
	     .ClType = "Verbless";
	   }
	
[P-ADV-S] P* ADV S -> CL
	? { "P-ADV-S" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Punc = 2.Punc;
	     .ClType = "Verbless"; 
	   }

// Randall 7/11/09 2co9:15:7
[P-ADV-ADV-S] P* ADV ADV S -> CL
	? { "P-ADV-ADV-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc; 
	     .ClType = "Verbless";
	   }
	
[P-ADV-S-ADV] P* ADV S ADV -> CL
	? { "P-ADV-S-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc; 
	     .ClType = "Verbless";
	   }
	
[ADV-P-ADV-S] ADV P* ADV S -> CL
	? { "ADV-P-ADV-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .Punc = 3.Punc; .ClType = "Verbless"; }

	
// VERBELIDED CLAUSES
[S-IO] S* IO -> CL
	? { "S-IO" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .Punc = 1.Punc; .ClType = "VerbElided"; }

// Randall 5/10/11 jms1:1
[S-IO-O] S* IO O -> CL
	? { "S-IO-O" in 0.AR && (2.End-0.Start) in 0.RB }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }
	
[S-ADV-IO] S* ADV IO -> CL
	? { "S-ADV-IO" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }

// Randall 5/28/09 mrk13:34:1-13:34:24
[S-ADV-IO-O] S* ADV IO O -> CL
	? { "S-ADV-IO-O" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc; .ClType = "VerbElided"; }
	
[S-ADV-ADV-IO] S* ADV ADV IO -> CL
	? { "S-ADV-ADV-IO" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc; .ClType = "VerbElided"; }
	
[S-IO-ADV] S* IO ADV -> CL
	? { "S-IO-ADV" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }

[S-IO-ADV-ADV] S* IO ADV ADV -> CL
	? { "S-IO-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc; .ClType = "VerbElided"; }
	
[ADV-S-IO] ADV S* IO -> CL
	? { "ADV-S-IO" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 2.Punc; .ClType = "VerbElided"; 
	   }
	   
[ADV-S-IO-ADV] ADV S* IO ADV -> CL
	? { "ADV-S-IO-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; .ClType = "VerbElided"; 
	   }
	   
[ADV-ADV-S-IO-ADV] ADV ADV S* IO ADV -> CL
	? { "ADV-ADV-S-IO-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc; .ClType = "VerbElided"; 
	   }
	   
[ADV-ADV-ADV-S-IO-ADV] ADV ADV ADV S* IO ADV -> CL
	? { "ADV-ADV-ADV-S-IO-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc; .ClType = "VerbElided"; 
	   }

[ADV-ADV-ADV-S-O-ADV] ADV ADV ADV S* O ADV -> CL
	? { "ADV-ADV-ADV-S-O-ADV" in 0.AR && ! ( (5.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc; .ClType = "VerbElided"; 
	   }
	   
[ADV-S-IO-ADV-ADV] ADV S* IO ADV ADV -> CL
	? { "ADV-S-IO-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 4.Punc; .ClType = "VerbElided"; 
	   }
	
[S-O] S* O -> CL
	? { "S-O" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .Punc = 1.Punc; .ClType = "VerbElided"; }
	
[O-S] O S*-> CL
	? { "O-S" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .Punc = 1.Punc; .ClType = "VerbElided"; 
	     .RB = 0.RB; .NB = 0.NB;
	   }
	
[ADV-S-O] ADV S* O -> CL
	? { "ADV-S-O" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 2.Punc; .ClType = "VerbElided"; 
	   }

[ADV-S-O-ADV] ADV S* O ADV -> CL
	? { "ADV-S-O-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 3.Punc; .ClType = "VerbElided"; 
	   }
	
[S-O-ADV] S* O ADV -> CL
	? { "S-O-ADV" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }

[S-O-ADV-ADV] S* O ADV ADV -> CL
	? { "S-O-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .Punc = 3.Punc; .ClType = "VerbElided"; }
	
[S-ADV-O] S* ADV O -> CL
	? { "S-ADV-O" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }
	
[S-ADV-ADV-ADV] S* ADV ADV ADV -> CL
	? { "S-ADV-ADV-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; .ClType = "VerbElided"; }
	
[O-ADV] O* ADV -> CL
	? { ( "O-PP" in 0.AR || "O-ADV" in 0.AR  )
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; .ClType = "VerbElided"; }
	
[O-ADV-ADV] O* ADV ADV -> CL
	? { "O-ADV-ADV" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }

// Randall 7/30/09 luk22:22:1
[O-ADV-ADV-ADV] O* ADV ADV ADV -> CL
	? { "O-ADV-ADV-ADV" in 0.AR
	  && (3.End-0.Start) in 0.RB
	  }
	>> { .Punc = 3.Punc; .ClType = "VerbElided"; }
	
[ADV-O-ADV] ADV O* ADV -> CL
	? { "ADV-O-ADV" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; }
	
[ADV-ADV-O-ADV] ADV ADV O* ADV -> CL
	? { "ADV-ADV-O-ADV" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 3.Punc; .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; }
	
[IO-ADV] IO* ADV -> CL
	? { "IO-ADV" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; .ClType = "VerbElided"; }

[IO-ADV-ADV] IO* ADV ADV -> CL
	? { "IO-ADV-ADV" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }
	
[O-IO] O* IO -> CL
	? { "O-IO" in 0.AR 
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; .ClType = "VerbElided"; }
	
[O-O2] O* O2 -> CL
	? { "O-O2" in 0.AR 
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; 
	     .ClType = "VerbElided";
	   }

// Randall 6/2/09 rev2:19
[O-O2-ADV] O* O2 ADV -> CL
	? { "O-O2-ADV" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; 
	     .ClType = "VerbElided";
	   }

//Randall 7/29/08 Rom 6:12:1
[O-O2-IO] O* O2 IO -> CL
	? { "O-O2-IO" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; 
	     .ClType = "VerbElided";
	   }
	   	
[ADV-O] ADV O* -> CL
	? { "ADV-O" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 1.Punc;
	   }
	
[ADV-IO] ADV IO* -> CL
	? { "ADV-IO" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 1.Punc;
	   }

// Randall 8/27/09 act11:19:1
[ADV-ADV-IO] ADV ADV IO* -> CL
	? { "ADV-ADV-IO" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc; .ClType = "VerbElided"; 
	   }
	
[ADV-IO-ADV] ADV IO* ADV -> CL
	? { "ADV-IO-ADV" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc; .ClType = "VerbElided"; 
	   }
	
[ADV-ADV] ADV ADV* -> CL
	? { "ADV-ADV" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .ClType = "VerbElided";
	     .Punc = 1.Punc;
	   }

[ADV-O-S] ADV O S* -> CL
	? { "ADV-O-S" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 2.Punc;
	   }
		
[ADV-IO-S] ADV IO S* -> CL
	? { "ADV-IO-S" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 2.Punc;
	   }

// Randall 8/20/11 ro16:27
[ADV-IO-S-ADV] ADV IO S* ADV -> CL
	? { "ADV-IO-S-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 3.Punc;
	   }
	   
[ADV-IO-ADV-S-ADV] ADV IO ADV S* ADV -> CL
	? { "ADV-IO-ADV-S-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc; .ClType = "VerbElided"; 
	   }
	
[ADV-ADV-ADV-S-ADV] ADV ADV ADV S* ADV -> CL
	? { "ADV-ADV-ADV-S-ADV" in 0.AR 
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 4.Punc; .ClType = "VerbElided"; 
	   }

[ADV-ADV-ADV] ADV* ADV ADV -> CL
	? { "ADV-ADV-ADV" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc; .ClType = "VerbElided"; 
	   }
	   
[ADV-ADV-ADV-ADV] ADV* ADV ADV ADV -> CL
	? { "ADV-ADV-ADV-ADV" in 0.AR 
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc; .ClType = "VerbElided"; 
	   }
	   	   
[IO-S] IO S* -> CL
	? { "IO-S" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 1.Punc;
	   }
	
[IO-S-ADV] IO S* ADV -> CL
	? { "IO-S-ADV" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc; .ClType = "VerbElided"; 
	   }

// Randall 6/11/09 eph3:20:1-3:21:18
[IO-S-ADV-ADV] IO S* ADV ADV -> CL
	? { "IO-S-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 3.Punc;
	   }
	
[IO-O] IO O* -> CL
	? { "IO-O" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 1.Punc;
	   }
	
[IO-O-ADV] IO O* ADV -> CL
	? { "IO-O-ADV" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 2.Punc;
	   }
	
[IO-O-ADV-ADV] IO O* ADV ADV -> CL
	? { "IO-O-ADV-ADV" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 3.Punc;
	   }

// Randall 12/27/13 2th1:7
[IO-O-ADV-ADV-ADV] IO O* ADV ADV ADV -> CL
	? { "IO-O-ADV-ADV-ADV" in 0.AR && ! ( (4.End-0.Start) in 0.NB ) }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 4.Punc;
	   }

[IO-ADV-S-ADV] IO ADV S* ADV -> CL
	? { "IO-ADV-S-ADV" in 0.AR && ! ( 3.End in 1.NE ) }
	>> { .RB = 0.RB; .NB = 0.NB; .Punc = 3.Punc; .ClType = "VerbElided"; }
	
[S2CL] S* -> CL
	? { "S2CL" in 0.AR 
        && ! ( 0.UnicodeLemma in {"ὅσος"} ) // Deals with stray S2CL in rev13:15 that causes crash (could not change dictionary at the time)
      }
	>> { .ClType = "VerbElided"; }
	
[O2CL] O* -> CL
	? { "O2CL" in 0.AR }
	>> { .ClType = "VerbElided";
	   }
	
[IO2CL] IO* -> CL
	? { "IO2CL" in 0.AR }
	>> { .ClType = "VerbElided";
	   }
	
[ADV2CL] ADV* -> CL
	? { "ADV2CL" in 0.AR 
		||( "Intj2CL" in 0.AR
		   && ( 0.Lemma in {"ou)"} || 0.UnicodeLemma in {"οὐ"} )
          )
        || ( "Ptcl2Intj" in 0.AR // to deal with SBLGNT morph on οὐχί
           && "Intj2CL" in 0.AR
           && ( "ClCl2" in 0.AR || "Conj2CL" in 0.AR || "CLaCL" in 0.AR )
           && 0.UnicodeLemma in {"οὐχί"}
           )
        || ( 0.Lemma in {"εἰ δὲ μή"}
             && 0.NormalizedForm in {"μήγε"}
           )
	  }
	>> { .ClType = "VerbElided";
	   }
	
[S-ADV] S* ADV -> CL
	? { ( "S-PP" in 0.AR || "S-ADV" in 0.AR ) 
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; .ClType = "VerbElided"; }
	
[S-ADV-ADV] S* ADV ADV -> CL
	? { ( "S-PP-ADV" in 0.AR || "S-ADV-ADV" in 0.AR )
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 2.Punc; .ClType = "VerbElided"; }

[ADV-S] ADV S* -> CL
	? { "ADV-S" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 1.Punc;
	   }

// Randall 8/24/09 heb10:18
[ADV-ADV-S] ADV ADV S* -> CL
	? { "ADV-ADV-S" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 2.Punc;
	   }
	
[ADV-ADV-ADV-S] ADV ADV ADV S* -> CL
	? { "ADV-ADV-ADV-S" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 3.Punc;
	   }
	
[ADV-S-ADV] ADV S* ADV -> CL
	? { ( "PP-S-PP" in 0.AR || "ADV-S-ADV" in 0.AR )
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 2.Punc; 
	     .ClType = "VerbElided"; }
	
[ADV-ADV-O] ADV ADV O* -> CL
	? { "ADV-ADV-O" in 0.AR 
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 2.Punc; 
	     .ClType = "VerbElided"; 
	   }

[O-IO-ADV-ADV-V] O IO ADV ADV V* -> CL
	? { "O-IO-ADV-ADV-V" in 0.AR
	  && ! ( (4.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .ClType = "VerbElided"; 
		 .Punc = 4.Punc;
	   }

		 
// CONJOINING CLAUSES (AT LEAST ONE CONJUNCTION)

// "that" content/object clauses
[that-VP] conj CL* -> CL
/******************Commented out because applied beyond "that" clauses**********
that-clause overused and so need to block when used with certain lemmas
Reserve that-VP for "ἵνα" and {"ὅτι"} object/content clauses "that" clauses
	? { ( ( 0.UnicodeLemma in {"ἵνα", "ἐάν"} // "that" "if" 
	      || 0.encode in {"ὅτι"} // "that"
	      )
	    && 0.Punc==""
	    && ( ! ( "that-VP" in 0.AR ) || (1.End-0.Start) in 0.RB )
	    && ! ( "that-VP" in 0.BR )
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	  || ( (1.End-0.Start) in 0.RB && "that-VP" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }
*********************************************************************************/
	? { ( ( 0.encode in {"i(/na","o(/ti"} || 0.UnicodeLemma in {"ἵνα","ὅτι"} ) // "that"
		&& 0.Punc==""
		&& ! ( "sub-CL" in 0.AR ) 
		&& ! ( "that-VP" in 0.BR )
		&& ! ( (1.End-0.Start) in 0.NB )
		)
	  || ( (1.End-0.Start) in 0.RB && "that-CL" in 0.AR )	  // "that-CL" to avoid old that-VP problem
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

/* Subordinate clauses except for object clauses
 Should be used for subordinate clauses introduced by subordinate conj,
 except for "ἵνα" and {"ὅτι"} clauses when they are object clauses 
 ("ἵνα" and {"ὅτι"} clauses that function as ADV also use sub-CL
 */	
[sub-CL] conj CL* -> CL     
	? { ( ( "sub-CL" in 0.AR || "that-VP" in 0.AR || "PtclCL" in 0.AR
		|| ( 0.Lemma in {"ei)","e)a/n","o(/te","o(/tan","w(/ste","o(/pws","dio/ti"} 
            || 0.UnicodeLemma in {"εἰ","ἐάν","ἐάν","ὅτε","ὅταν","ὥστε","ὅπως","διότι","ἐάνπερ","ἐάνπερ"}  ) // ἐάνπερ is under e)a/n in the lemma
		 )		     
	  && ( (1.End-0.Start) in 0.RB || 0.Lemma in {"e)a/n"} || 0.UnicodeLemma in {"ἐάν","ἐάν"} )
	  && ! ( 0.UnicodeLemma in {"δέ","γάρ","ἀλλά","καί","μέν","τέ","οὖν","ὅθεν","διό","τοιγαροῦν","τοίνυν","τοίνυν","μή","δέ","γάρ","ἀλλά","καί","μέν","τέ","ὅθεν","διό","τοίνυν","μή","μή"} )
	  && ! ( "sub-CL" in 0.BR )
	  && ! ( (1.End-0.Start) in 0.NB )
	  && ! ( 0.UnicodeLemma in {"ἄρα"}  // gal3:29:1-3:29:12
			 && "Conj2Ptcl" in 0.AR
			 && "PtclCL" in 0.AR
		   )
      && ! ( 0.NA27Unicode in {"μήποτε","μήποτέ"}
             && 0.Lemma in {"πότε"}
           )
        )
	  || ( (1.End-0.Start) in 0.RB && "sub-CL" in 0.AR 
         && ! ( 0.NA27Unicode in {"μήποτε","μήποτέ"}
               && 0.Lemma in {"πότε"}
              )
         )
	  || ( "AdvpCL" in 0.AR		
		 && 0.UnicodeLemma in {"πρίν"} ) // Logos morph has pri/n as conj rather than adv
      || ( 0.UnicodeLemma in {"ἐάν"} && 1.UnicodeLemma in {"ἔρχομαι"} && 1.Unicode in {"ἔλθω,"} // Address 3jn1:10 case (at the time could not change dictionary)
         )
      || ( 0.Lemma in {"εἰ"}
           && 1.Lemma in {"εἰ δὲ μή"}
           && 1.NormalizedForm in {"μήγε"}
         )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

// Clauses conjoined with a coordinating conj #
[Conj-CL] conj CL* -> CL
	? { ( !Disjoint( {"Conj-CL", "that-VP"}, 0.AR )
		&& ( 0.Lemma in {"de/","ga/r","a)lla/","kai/","me/n","te/","ou)=n","o(/qen","dio/","toigarou=n","toi/nun"} 
            || 0.UnicodeLemma in {"δέ","δέ","γάρ","γάρ","ἀλλά","ἀλλά","καί","καί","μέν","μέν","τέ","τέ","οὖν","ὅθεν","διό","διό","τοιγαροῦν","τοίνυν","τοίνυν","τοίνυν"} )
		&& (1.End-0.Start) in 0.RB
		&& ! ( "sub-CL" in 0.AR )
		&& ! ( 0.UnicodeLemma in {"εἰ","ἐάν","ὅτε","ὅταν","ὥστε","ὅπως","διότι","ἐάν","διότι","ἐάνπερ","ἐάνπερ"} ) // addresses JT & SBL
		&& ! ( "Conj-CL" in 0.BR )
		&& ! ( (1.End-0.Start) in 0.NB )
		)
	  || ( (1.End-0.Start) in 0.RB && "Conj-CL" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }

/*Covers both conjunctive & disjunctive right now; 
disjunctive alla, de, plhn should be distinguished later*/
[CLaCL] CL* conj CL -> CL
	? { (2.End-0.Start) in 0.RB && ( "Conj2CL" in 0.AR || "CLaCL" in 0.AR ) 
	  && ! ( "sub-CL" in 1.AR )
	  && ! ( "Conj2CL-1" in 2.BR || "CLaCL-1" in 2.BR )
	  && ! ( 1.UnicodeLemma in {"ἕως","οὖν","ὅθεν","διό","τοιγαροῦν","τοίνυν","εἰ","ἐάν","ὅτε","ὅταν","ὥστε","ὅπως","διότι","ἕως","διό","τοίνυν","ἐάν","διότι"} )
	  && ! ( 1.UnicodeLemma in {"γάρ","ἵνα","ὅπου","ἄρα","ὡς","ὅτι","γάρ"} ) // addresses JT & SBL
	  }
	>> { .Punc = 2.Punc; .Coord = true; 
  //       .Relative = false && ! ( 0.Type in {"Relative"} && 2.Type in {"Relative"} ); // Caused some good trees like Mat10:38 to break down
       }

// Randall 8/17/09 2co6:8:9
[CLandCL2] CL conj CL* -> CL
	? { (2.End-0.Start) in 0.RB && "CLandCL2" in 0.AR 
	  }
	>> { .RB = 0.RB; .NB = 0.NB; .Punc = 2.Punc; .Coord = true; 
    //     .Relative = false && ! ( 0.Type in {"Relative"} && 2.Type in {"Relative"} );
       }

[CLa2CL] CL* conj CL CL -> CL
	? { (3.End-0.Start) in 0.RB && ( "CLandClCl" in 0.AR || "CLa2CL" in 0.AR ) 
	  }
	>> { .Punc = 3.Punc; .Coord = true; }

// Randall 6/13/09 mat11:5:1	
[CLandClClandClandClandCl] CL* conj CL CL conj CL conj CL conj CL -> CL
	? { (9.End-0.Start) in 0.RB && "CLandClClandClandClandCl" in 0.AR 
	  }
	>> { .Punc = 9.Punc; .Coord = true; }

[2CLaCL] CL* CL conj CL -> CL
	? { (3.End-0.Start) in 0.RB && ( "ClClandCl" in 0.AR || "2CLaCL" in 0.AR ) }
	>> { .Punc = 3.Punc; .Coord = true; }
			
[Conj3CL] CL* conj CL conj CL -> CL
	? { (4.End-0.Start) in 0.RB && "Conj3CL" in 0.AR 
	  && ! ( "Conj3CL-1" in 4.BR )
	  }
	>> { .Punc = 4.Punc; .Coord = true; }
	
[Conj4CL] CL* conj CL conj CL conj CL -> CL
	? { (6.End-0.Start) in 0.RB && "Conj4CL" in 0.AR }
	>> { .Punc = 6.Punc; .Coord = true; }

[2CLaCLaCL] CL* CL conj CL conj CL -> CL
	? { (5.End-0.Start) in 0.RB && ( "ClClandClandCl" in 0.AR || "2CLaCLaCL" in 0.AR ) }
	>> { .Punc = 5.Punc; .Coord = true; }

// Randall 6/3/08 for Heb 6:4:1 58006004004
[Conj5CL] CL* conj CL conj CL conj CL conj CL -> CL
	? { (8.End-0.Start) in 0.RB && "Conj5CL" in 0.AR }
	>> { .Punc = 8.Punc; .Coord = true; }

// Randall 11/7/08 for Rom 2:17:1 45002017003
[Conj6CL] CL* conj CL conj CL conj CL conj CL conj CL -> CL
	? { (10.End-0.Start) in 0.RB && "Conj6CL" in 0.AR }
	>> { .Punc = 10.Punc; .Coord = true; }

// Randall 5/30/09 2pe1:5:1-1:7:12
[Conj7CL] CL* conj CL conj CL conj CL conj CL conj CL conj CL -> CL
	? { (12.End-0.Start) in 0.RB && "Conj7CL" in 0.AR }
	>> { .Punc = 12.Punc; .Coord = true; }

// Randall 6/12/09 Mat1:12:1-1:16:15
[Conj12CL] CL* conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL -> CL
	? { (22.End-0.Start) in 0.RB && "Conj12CL" in 0.AR }
	>> { .Punc = 22.Punc; .Coord = true; }

// Randall 6/12/09 Mat1:2:1-1:6:7
[Conj13CL] CL* conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL -> CL
	? { (24.End-0.Start) in 0.RB && "Conj13CL" in 0.AR }
	>> { .Punc = 24.Punc; .Coord = true; }

// Randall 6/12/09 mat1:6:8-1:11:13
[Conj14CL] CL* conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL conj CL -> CL
	? { (26.End-0.Start) in 0.RB && "Conj14CL" in 0.AR }
	>> { .Punc = 26.Punc; .Coord = true; }

[aCLaCL] conj CL* conj CL -> CL
	? { (3.End-0.Start) in 0.RB && ( "EitherOrCL" in 0.AR || "aCLaCL" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 3.Punc; .Coord = true; }

// Randall 6/4/08 for Mat13:23:1
[aCLaCLaCL] conj CL* conj CL conj CL -> CL
	? { (5.End-0.Start) in 0.RB && ( "EitherOr3CL" in 0.AR || "aCLaCLaCL" in 0.AR )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 5.Punc; .Coord = true; }

// Randall 11/14/08 for 1co1:12:1-1:12:20  46001012009
[EitherOr4CL] conj CL* conj CL conj CL conj CL -> CL
	? { (7.End-0.Start) in 0.RB && "EitherOr4CL" in 0.AR
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 7.Punc; .Coord = true; }

// Randall 4/29/09 1co12:8:1
[EitherOr7CL] conj CL* conj CL conj CL conj CL conj CL conj CL conj CL -> CL
	? { (13.End-0.Start) in 0.RB && "EitherOr7CL" in 0.AR
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
	     .Punc = 13.Punc; .Coord = true; }


// CLAUSE COORDINATION RULES (WITHOUT CONJUNCTIONS)

// Unlike other clause coordination rules, head cl important (first clause is head)	
[ClCl] CL* CL -> CL
	? { (1.End-0.Start) in 0.RB
	  && "ClCl" in 0.AR
	  &&! ( "ClCl-1" in 1.BR )
	  &&! ( ( 0.Relative || 0.Type=="Relative") || 0.Rule in {"Np2CL","Intj2CL"} )
	  || "ClCl-1" in 1.AR
		 && ! ( (1.End-0.Start) in 0.NB )
	  || 0.Rule in {"Np2CL","Intj2CL"} 
		 && 1.Rule in {"Np2CL","Intj2CL"}
		 && ! ( 0.Rule in {"Np2CL"} && 1.Rule in {"Intj2CL"} 
                && ( 1.Lemma in { "i)dou/", "i)/de" } || 1.UnicodeLemma in {"ἰδού","ἴδε"} )
               )
		 && ! ( (1.End-0.Start) in 0.NB )
		 && ! ( ( "NP-Appos" in 0.AR || "Np-Appos" in 0.AR ) && (1.End-0.Start) in 0.RB )
		 && ! ( "ClCl-1" in 1.BR 
                && ( 0.Lemma=="ku/rios" || 0.UnicodeLemma in {"κύριος","κύριος"} )
                && ( 1.Lemma=="i)hsou=s" || 1.UnicodeLemma in {"Ἰησοῦς"} )
               )                                    // act7:59:1
	  }
	>> { .Punc = 1.Punc; }

// Unlike other clause coordination rules, head cl important (second clause is head)	
[ClCl2] CL CL* -> CL
	? { ( (1.End-0.Start) in 0.RB
	  && "ClCl2" in 0.AR
	  &&! ( "ClCl2-1" in 1.BR )
	  &&! ( 0.Rule in {"Np2CL","Intj2CL"} && 1.Rule in {"Np2CL","Intj2CL"} )
        )
	  || ( (1.End-0.Start) in 0.RB
		 && "ClCl" in 0.AR
		 && ( ( ( 0.Relative || 0.Type=="Relative") || 0.Rule in {"Np2CL","Intj2CL"} 
			  && ! ( 1.Rule in {"Np2CL","Intj2CL"} )
			  )
			)
		 &&! ( "ClCl-1" in 1.AR )
		 &&! ( "ClCl2-1" in 1.BR )
		 &&! ( (1.End-0.Start) in 0.NB )
         )
	  || ( "ClCl2-1" in 1.AR
		 && ! ( (1.End-0.Start) in 0.NB )
         )  
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Punc = 1.Punc;
		 if ( (1.End-0.Start) in 0.RB
			  && "ClCl" in 0.AR
			  && ( ( 0.Relative || 0.Type=="Relative") || 0.Rule in {"Np2CL","Intj2CL"} )
			)
		 { .AR = 0.AR; .BR = 0.BR; .RB = 0.RB; .NB = 0.NB; }
	   }
	
[ClClCl] CL* CL CL -> CL
	? { (2.End-0.Start) in 0.RB
	  && "ClClCl" in 0.AR
	  && ! ( "ClClCl-1" in 2.BR ) // act13:26:1
	  }
	>> { .Punc = 2.Punc; .Coord = true; }
	
[ClClClCl] CL* CL CL CL -> CL
	? { (3.End-0.Start) in 0.RB
	  && "ClClClCl" in 0.AR
	  }
	>> { .Punc = 3.Punc; .Coord = true; }
	
[ClClClClCl] CL* CL CL CL CL -> CL
	? { (4.End-0.Start) in 0.RB
	  && "ClClClClCl" in 0.AR
	  }
	>> { .Punc = 4.Punc; .Coord = true; }
	
[ClClClClClCl] CL* CL CL CL CL CL -> CL
	? { (5.End-0.Start) in 0.RB
	  && "ClClClClClCl" in 0.AR
	  }
	>> { .Punc = 5.Punc; .Coord = true; }

// Randall 5/28/09 php3:5:1-3:6:1
[ClClClClClClCl] CL* CL CL CL CL CL CL -> CL
	? { (6.End-0.Start) in 0.RB
	  && "ClClClClClClCl" in 0.AR
	  }
	>> { .Punc = 6.Punc; .Coord = true; }

// Randall 5/23/09 2co11:26:1-11:27:17
[ClClClClClClClCl] CL* CL CL CL CL CL CL CL -> CL
	? { (7.End-0.Start) in 0.RB
	  && "ClClClClClClClCl" in 0.AR
	  }
	>> { .Punc = 7.Punc; .Coord = true; }

// Randall 2/10/09 for Heb11:32:5-11:34:16
[ClClClClClClClClCl] CL* CL CL CL CL CL CL CL CL -> CL
	? { (8.End-0.Start) in 0.RB
	  && "ClClClClClClClClCl" in 0.AR
	  }
	>> { .Punc = 8.Punc; .Coord = true; }

// Randall 8/14/09 rom 12:10
[ClClClClClClClClClCl] CL* CL CL CL CL CL CL CL CL CL -> CL
	? { (9.End-0.Start) in 0.RB
	  && "ClClClClClClClClClCl" in 0.AR
	  }
	>> { .Punc = 9.Punc; .Coord = true; }

// Randall 6/4/09 rev7:5:1-7:8:16
[ClClClClClClClClClClClCl] CL* CL CL CL CL CL CL CL CL CL CL CL -> CL
	? { (11.End-0.Start) in 0.RB
	  && "ClClClClClClClClClClClCl" in 0.AR
	  }
	>> { .Punc = 11.Punc; .Coord = true; }

/* Commented out because this rule is of doubtful application
and has apparently been previously abandoned in any case
// Complement clauses
[P-V-O] P V* O -> CL
	? { "P-V-O" in 0.AR
	  && ! ( (2.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; }
*/

/* Doubtful that VC ClType clauses should have IO
Used only in Luk1:14:1 (incorrectly) and so removed	   
[VC-IO-P] VC IO P* -> CL
	? { ( ! ( "VC-IO-P" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc==""
	    && ! ( (2.End-0.Start) in 0.NB )
	    )
	  || ( "VC-IO-P" in 0.AR && ! ( (2.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Mood = 0.Mood;
	   }
*/

/* Doubtful that VC ClType clauses should have IO
Used only in Luk1:7:1 (and incorrectly) and so removed	   
[ADV-VC-IO-S] ADV VC* IO S -> CL
	? { ( ! ( "ADV-VC-IO-S" in 0.BR ) 
	    && 0.Punc=="" && 1.Punc=="" && 2.Punc==""
	    && ! ( (3.End-0.Start) in 0.NB )
	    )
	  || ( "ADV-VC-IO-S" in 0.AR && ! ( (3.End-0.Start) in 0.NB ) )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	     .Punc = 3.Punc;
	   }
*/

/* Commented out because VC ClType clauses need P
Previously applied only once--in Mrk8:1:1
Removed and changed to ADV-ADV-S-V (the "to be" being existential)
[ADV-ADV-S-VC] ADV ADV S* VC -> CL
	? { "ADV-ADV-S-VC" in 0.AR
	  && ! ( (3.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB;
		 .Mood = 3.Mood 
	     .Punc = 3.Punc;
	   }
*/

/*********************************
Reason for removal: Decided to represent every participle a clause in 
themselves so all participles go to a CL and relate to np by CL2Adjp
// Participle used as an adjective
[V2Adjp] verb* -> adjp
	? { 0.Mood=="Participle"
	  && "V2Adjp" in 0.AR 
	  }
*********************************/

/* Previously only used twice; in 1Th1:8:1 and 1Th2:1:1
Consider grouping in new rule with different ClType of apposition (non-identifying)
Temporarily grouped with Np-Appos
[NP-Adjunct] np* np -> np
	? { "NP-Adjunct" in 0.AR
	  && (1.End-0.Start) in 0.RB
	  }
*/

/* Np post-modified by a participial phrase
Previously only used in Jhn1:35:1 and so phased out completely
[NP-VPing] np* vp -> np
	? { (1.End-0.Start) in 0.RB && "NP-VPing" in 0.AR }
	>> { .Punc = 1.Punc; }
*/

/* Previously used only twice; in 2Cor4:13:1; Mat24:38:1
and so phased out completelu;
The rule used to be more extensively applied automatically,
but had previously been commented out except for manual application
[VP-VPing] vp* vp -> vp
	? { (1.End-0.Start) in 0.RB && "VP-VPing" in 0.AR }
	>> { .Punc = 1.Punc; }
*/
/*	? { ( 1.Mood == "Participle" 
	    && !1.HasNom
	    && !0.HasNom
	    && 0.Mood != "Participle"
	    && ! (0.Rule in {"AccVPDat", "AdvPBeNP", "VP-VPing", "VP-PP"} )
	    && ! ( 1.Rule in {"PP-VP"} )
	    && ! ( "VP-VPing" in 0.BR )
	    )
	  || ( (1.End-0.Start) in 0.RB && "VP-VPing" in 0.AR )
	  } */

/* VP with genitive np; not used at all and so commented out
the gen np simply tagged as O or ADV in all cases 
[VP-Gen] vp* np -> vp
	? { 1.Case=="Genitive"
	  && "VP-Gen" in 0.AR
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .Punc = 1.Punc; }
*/

/* Cl premodified by prep functioning as CL
Previously used only 5 times; in Mrk3:27:1; 5:3:8; 9:9:1; Mat1:18:9; 12:17:1
Merged with PrepCL rule above
[ppCL] prep CL* -> CL
	? { "ppCL" in 0.AR 
	  && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { .RB = 0.RB; .NB = 0.NB; 
	   }
*/

// CL premodified by prep functioning as pp	   
/* Lots of misapplied places with KATHWS, hWS subordinate clauses; 100 occurrences of total usage
Places here CL legitimately governed by a preposition involve the CL as np; merged with PrepNp rule
[PrepCL] prep CL* -> pp
	? { ( "PrepCL" in 0.AR || "ppCL" in 0.AR )
	    && ! ( (1.End-0.Start) in 0.NB )
	  }
	>> { if ( 0.Lemma == "u(po/" || 0.UnicodeLemma == "ὑπό" ) .ByPhrase = true; 
	     .PrepLemma = 0.UnicodeLemma;
	     .RB = 0.RB; .NB = 0.NB;
	   }

*/

/* Commented out as decision made that Prep goes with an np (so PrepCL also to be commented out)
// Adjp premodified by prep
[PrepAdjp] prep adjp* -> pp
	? { ( "PrepAdjp" in 0.AR 
	    && ! ( (1.End-0.Start) in 0.NB )
	    )
	    || ( ( 1.Lemma in {"me/sos"} // ,"dexio/s","eu)w/numos"} || 1.UnicodeLemma in {"μέσος"} // ,"δεξιός","εὐώνυμος"} )
	       &&! ( 1.Rule in {"DetAdj"} )
	       &&! ( "PrepAdjp" in 0.BR )
     	   && ! ( (1.End-0.Start) in 0.NB )
	       )
	  }
	>> { .PrepLemma = 0.UnicodeLemma;
		 .RB = 0.RB; .NB = 0.NB;
		 .Case = ""; 
	   }
*/

/**Commented out as postpositive conjunctions moved and no need for conj absorption**
[VerbConj] verb* conj -> verb
	? { "VerbConj" in 0.AR && ! ( (1.End-0.Start) in 0.NB ) }
	>> { .Punc = 1.Punc; }

[ConjVerb] conj verb* -> verb
	? { "ConjVerb" in 0.AR && ! ( (1.End-0.Start) in 0.NB) }
	>> { .RB = 0.RB; .NB = 0.NB; }

[ConjAdj] conj adj* -> adj
	? { "ConjAdj" in 0.AR && ! ( (1.End-0.Start) in 0.NB) }
	>> { .RB = 0.RB; .NB = 0.NB;  }

[PrepConj] prep* conj -> prep
	? { "PrepConj" in 0.AR && ! ( (1.End-0.Start) in 0.NB) }
	>> { .Punc = 1.Punc; }

[NpConj] np* conj -> np
	? { "NpConj" in 0.AR }
	>> { .Punc = 1.Punc; }
*************************************************************************************/

/****************************	
Commented out previously apparently because of redundancy with similar rules with
different names  
[PP-PP] pp* pp -> pp
	? { (1.End-0.Start) in 0.RB && "PP-PP" in 0.AR }
	>> { .Punc = 1.Punc; }
	
[PP-PP-PP] pp* pp pp -> pp
	? { (2.End-0.Start) in 0.RB && "PP-PP-PP" in 0.AR }
	>> { .Punc = 2.Punc; }
	
[PP-PP-PP-PP] pp* pp pp pp -> pp
	? { (3.End-0.Start) in 0.RB && "PP-PP-PP-PP" in 0.AR }
	>> { .Punc = 3.Punc; }
*******************************/

<PropagationExclusion>
Coord
Notes
Ref
SubjRef
ClType

<XmlFormat>
Cat:attribute
Rule:attribute
English:attribute
Chinese:attribute
Person:attribute
Number:attribute
Gender:attribute
Case:attribute
Degree:attribute
Tense:attribute
Voice:attribute
Mood:attribute	
encode:attribute
Unicode:attribute
Lemma:attribute
Personal:attribute
Demonstrative:attribute
Relative:attribute
Interrogative:attribute
morphId:attribute
StrongNumber:attribute
Start:attribute
End:attribute
HasNom:attribute
HasAcc:attribute
HasDat:attribute
HasDet:attribute
PrepLemma:attribute
RB:attribute
NB:attribute
AR:attribute
BR:attribute
Substantive:attribute
Head:attribute
Punc:attribute
Neg:attribute
Label:attribute
Notes:attribute
ClType: attribute
Type: attribute
SubClType: attribute
RefIndex:attribute
Chunk:attribute
Coord:attribute
Language:attribute
UnicodeLemma:attribute
StrongNumberX:attribute
MorphId2:attribute
gntNumber: attribute
morph: attribute
altMorph: attribute
functionMorph: attribute
Suffix: attribute
Prefix: attribute
ParagraphBreak: attribute
SectionBreak: attribute
Ref: attribute
SubjRef: attribute
Frame: attribute
lbsSurface: attribute
lbsLemma: attribute
NounType: attribute
FunctionalTag: attribute
FormalTag: attribute
TVM_Number: attribute
NormalizedForm: attribute
ManuscriptID: attribute
NA27Unicode: attribute
Role: attribute
EmbeddedRole: attribute
AdjunctType: attribute
PrepositionType: attribute
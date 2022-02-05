In the last experiment, the Max similarity of StraightCross to AngleCross, was more than its Max similarity to other StraightCross shapes(the shapes from its own category).
Assuming that the SpatialpPooler was not getting its entry from enough amount of PotencialConnections in each entry area, the PotencialRadious in 
Json file is increased from 30 (in last experiment) to 60. Hoping with making this Increament, the SpatialPooler would be generating more reliable 
SDRs for the next Layers.

Result : the mentioned issue is resolved. but the maximum similarity in each category of the shapes(entries), is extreamly decreased.
 
the plan for next experiment is to make another huge increament in PotencialRadius, just to get a better understanding
 of dependance of the results to the value of PotencialRadius.
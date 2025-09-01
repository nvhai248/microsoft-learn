import { Routes } from '@angular/router';
import {AuthComponent} from './components/auth.component/auth.component';
import {GameComponent} from './components/game.component/game.component';
import {WishlistComponent} from './components/wishlist.component/wishlist.component';
import {AuthGuard} from './guards/auth.guard';

export const routes: Routes = [
  {path: "", component: AuthComponent},
  {path: "games", component: GameComponent, canActivate: [AuthGuard]},
  {path: "wishlist", component: WishlistComponent, canActivate: [AuthGuard]},
];
